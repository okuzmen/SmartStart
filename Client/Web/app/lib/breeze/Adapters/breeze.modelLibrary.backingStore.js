﻿"use strict";
(function (factory) {
    if (breeze) {
        factory(breeze);
    } else if (typeof require === "function" && typeof exports === "object" && typeof module === "object") {
        // CommonJS or Node: hard-coded dependency on "breeze"
        factory(require("breeze"));
    } else if (typeof define === "function" && define["amd"] && !breeze) {
        // AMD anonymous module with hard-coded dependency on "breeze"
        define(["breeze"], factory);
    }
}(function(breeze) {
    
    var core = breeze.core;

    var ctor = function() {
        this.name = "backingStore";
    };
    
    ctor.prototype.initialize = function() {

    };

    ctor.prototype.getTrackablePropertyNames = function (entity) {
        var names = [];
        for (var p in entity) {
            if (p === "entityType") continue;
            if (p === "_$typeName") continue;
            if (p === "_pendingSets") continue;
            if (p === "_backingStore") continue;
            var val = entity[p];
            if (!core.isFunction(val)) {
                names.push(p);
            }
        }
        return names;
    };

    ctor.prototype.initializeEntityPrototype = function (proto) {

        proto.getProperty = function(propertyName) {
            return this[propertyName];
        };

        proto.setProperty = function (propertyName, value) {
            if (!this._backingStore.hasOwnProperty(propertyName)) {
                throw new Error("Unknown property name:" + propertyName);
            }
            this[propertyName] = value;
            // allow setProperty chaining.
            return this;
        };

        //// called after any create during a query;
        // this method cannot be called while a 'defineProperty' accessor is executing
        // because of IE bug 
        proto.initializeFrom = function(rawEntity) {
            // copy unmapped properties from newly created client entity to the rawEntity.
            var that = this;
            this.entityType.unmappedProperties.forEach(function(prop) {
                var propName = prop.name;
                rawEntity[propName] = that[propName];
            });
            // this._backingStore = rawEntity;
            if (!this._backingStore) {
                this._backingStore = { };
            }
        };

        // internal implementation details - ugly because of IE9 issues with defineProperty.

        proto._pendingSets = [];
        proto._pendingSets.schedule = function(entity, propName, value) {
            this.push({ entity: entity, propName: propName, value: value });
            if (!this.isPending) {
                this.isPending = true;
                var that = this;
                setTimeout(function() { that.process(); });
            }
        };
        proto._pendingSets.process = function() {
            if (this.length === 0) return;
            this.forEach(function(ps) {
                if (!ps.entity._backingStore) {
                    ps.entity._backingStore = { };
                }
                ps.entity._backingStore[ps.propName] = ps.value;
            });
            this.length = 0;
            this.isPending = false;
        };

        movePropDefsToProto(proto);

    };

    // entity is either an entity or a complexObject
    ctor.prototype.startTracking = function (entity, proto) {
        // can't touch the normal property sets within this method - access the backingStore directly instead. 
        proto._pendingSets.process();
        var bs = movePropsToBackingStore(entity);

        // assign default values to the entity
        var stype = entity.entityType || entity.complexType;
        stype.getProperties().forEach(function(prop) {
            var propName = prop.name;
            var val = entity[propName];

            if (prop.isDataProperty) {
                if (prop.isComplexProperty) {
                    // TODO: right now we create Empty complexObjects here - these should actually come from the entity
                    if (prop.isScalar) {
                        val = prop.dataType._createInstanceCore(entity, prop);
                    } else {
                        val = breeze.makeComplexArray([], entity, prop);
                    }
                } else if (!prop.isScalar) {
                    val = breeze.makePrimitiveArray([], entity, prop);
                } else if (val === undefined) {
                    val = prop.defaultValue;
                }
                bs[propName] = val;
            } else if (prop.isNavigationProperty) {
                if (val !== undefined) {
                    throw new Error("Cannot assign a navigation property in an entity ctor.: " + prop.Name);
                }
                if (prop.isScalar) {
                    // TODO: change this to nullstob later.
                    bs[propName] = null;
                } else {
                    bs[propName] = breeze.makeRelationArray([], entity, prop);
                }
            } else {
                throw new Error("unknown property: " + propName);
            }
        });
    };


    // private methods

    function movePropDefsToProto(proto) {
        var stype = proto.entityType || proto.complexType;
        stype.getProperties().forEach(function(prop) {
            var propName = prop.name;
            if (!proto[propName]) {
                Object.defineProperty(proto, propName, makePropDescription(prop));
            }
        });

    }

    // this method cannot be called while a 'defineProperty' accessor is executing
    // because of IE bug mentioned above.

    function movePropsToBackingStore(instance) {
        var proto = Object.getPrototypeOf(instance);
        if (!instance._backingStore) {
            instance._backingStore = { };
        }
        var stype = proto.entityType || proto.complexType;
        stype.getProperties().forEach(function(prop) {
            var propName = prop.name;
            if (!instance.hasOwnProperty(propName)) return;
            var value = instance[propName];
            delete instance[propName];
            instance[propName] = value;
        });
        return instance._backingStore;
    }

    function makePropDescription(property) {
        var propName = property.name;
        var getAccessorFn = function(backingStore) {
            return function() {
                if (arguments.length == 0) {
                    return backingStore[propName];
                } else {
                    backingStore[propName] = arguments[0];
                }
            };
        };
        return {
            get: function() {
                var bs = this._backingStore;
                if (!bs) {
                    this._pendingSets.process();
                    bs = this._backingStore;
                    if (!bs) return;
                }
                return bs[propName];
            },
            set: function(value) {
                var bs = this._backingStore;
                if (!bs) {
                    this._pendingSets.schedule(this, propName, value);
                    return;
                }
                var accessorFn = getAccessorFn(bs);
                if (this._$interceptor) {
                    this._$interceptor(property, value, accessorFn);

                } else {
                    accessorFn(value);
                }
            },
            enumerable: true,
            configurable: true
        };
    }

    breeze.config.registerAdapter("modelLibrary", ctor);

}));
