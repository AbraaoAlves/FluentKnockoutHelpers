﻿define(['utility/typeMetadataHelper'],
function (typeMetadataHelper) {
    return function (apiTechProduct /*undefined on add since we don't know what type the user will pick*/) {
        var self = this;

        if (!apiTechProduct)
            self.$type = ko.observable(); //type will be set once it is choosen
        else 
            //here NO custom mappings being performed here but we want a
            //javascript representation (on 'this') of a C# TechProduct
            ko.mapping.fromJS(apiTechProduct, {}, self);


        self.productType = ko.computed({
            read: function () {
                //what type of product is this?
                return typeMetadataHelper.getTypeName(self);
            },
            write: function (typeName) {
                //UI requesting to change the type. find the type in typeMetadata, assign it to "this" and wire up validation internally
                typeMetadataHelper.getInstanceAndAssign(typeName, self);
            }
        });

        self.isProductTypeSet = ko.computed(function () {
            return self.productType() !== null;
        });

        //#region Computer
        self.isDesktop = ko.computed(function () {
            return typeMetadataHelper.isType(self, 'Desktop');
        });
        
        self.isLaptop = ko.computed(function () {
            return typeMetadataHelper.isType(self, 'Laptop');
        });

        self.isComputer = ko.computed(function() {
            return self.isDesktop() || self.isLaptop();
        });
        
        var computerBasicSpecs = ko.computed(function () {
            if (self.isProductTypeSet() && self.isComputer())
                return self.Mhz() + " Mhz Processor w/ " + self.GigsOfRam() + " GB RAM and " + (self.HasSsd() ? "an SSD" : "no SSD (bummer)");
        });
        //#endregion


        //#region Digital camera
        self.isPointAndShoot = ko.computed(function () {
            return typeMetadataHelper.isType(self, 'PointAndShoot');
        });

        self.isSlr = ko.computed(function () {
            return typeMetadataHelper.isType(self, 'Slr');
        });

        self.isDigitalCamera = ko.computed(function () {
            return self.isPointAndShoot() || self.isSlr();
        });

        var digitalCameraBasicSpecs = ko.computed(function () {
            if (self.isProductTypeSet() && self.isDigitalCamera())
                return self.MegaPixels() + ' Megapixels';
        });
        //#endregion


        //#region Summary Display Information
        self.productTypeDisplay = ko.computed(function () {
            if (self.isDesktop())
                return "Desktop";
            
            if (self.isLaptop())
                return "Laptop";
            
            if (self.isSlr())
                return "SLR";
            
            if (self.isPointAndShoot())
                return "Point & Shoot";
        });

        self.specSummary = ko.computed(function() {
            if(self.isComputer())
                return computerBasicSpecs();
            
            if (self.isDigitalCamera())
                return digitalCameraBasicSpecs();
        });
        //#endregion
    };
});