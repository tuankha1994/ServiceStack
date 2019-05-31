/* global angular */
(function () {
    "use strict";

    var app = angular.module("contact.module", ["kendo.directives"]);

    app.controller('contactCtrl', function ($scope, $http, $timeout) {
        //Init total record
        $scope.totalRecord = 0;
        $http.get("/contacts/totalRecord?format=json")
            .then(function (response) {
                $scope.totalRecord = response.data.total_record;
            });

        //Show data
        $timeout(function () {
            //grid main
            $scope.contactsGridOptions = {
                sortable: true,
                selectable: false,
                dataSource: contactsDataSource,
                pageable: true,
                scrollable: false,
                toolbar: [{ name: "create", text: "Add new contact" }],
                filterable: {
                    mode: "row"
                },
                columns: [
                    { field: "name", title: "Name", width: "40%" },
                    { field: "address", title: "Address", width: "30%", filterable: false },
                    { command: ["edit", "destroy"], title: "&nbsp;", width: "30%" }
                ],
                editable: "inline"
            };

            //grid detail
            $scope.contactDetailGridOptions = function (contact) {
                return {
                    sortable: true,
                    selectable: false,
                    dataSource: getDataSourcePhoneNumbersByContactID(contact.id),
                    pageable: false,
                    scrollable: false,
                    toolbar: [{ name: "create", text: "Add new number" }],
                    columns: [
                        { field: "number", title: "Number" },
                        { field: "type", title: "Type", editor: phoneNumberTypeDropDownEditor },
                        { command: ["edit", "destroy"], title: "&nbsp;", width: "300px" }
                    ],
                    editable: "inline"
                };
            };
        }, 500);

        //Config data source list contact
        var contactsDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    // server paging
                    $http.get("/contacts/" + e.data.skip + "/" + e.data.take + "?format=json")
                        .then(function (response) {
                            // on success
                            e.success(response.data);
                        });
                },
                create: function (e) {
                    //create model data update
                    var model = { "name": e.data.name, "address": e.data.address };
                    //call api update
                    $http.post("/contacts", model)
                        .then(function (response) {
                            if (response.data.status == "Existed") {
                                // on failure
                                e.error("XHR response", "111", "Contact name " + e.data.name + " already exists");
                            }
                            else {
                                // on success
                                e.success(response.data.contact);
                            }
                        });
                },
                update: function (e) {
                    //create model data update
                    var model = { "name": e.data.name, "address": e.data.address };
                    //call api update
                    $http.put("/contacts/" + e.data.id + "?format=json", model)
                        .then(function (response) {

                            if (response.data.status == "NotExisted") {
                                // on failure
                                e.error("XHR response", "111", "Contact name " + e.data.name + " does not exists");
                            }
                            else if (response.data.status == "Existed") {
                                // on failure
                                e.error("XHR response", "111", "Contact name " + e.data.name + " has been used by another contact");
                            }
                            else {
                                // on success
                                e.success();
                            }
                        });
                },
                destroy: function (e) {
                    //call api delete
                    $http.delete("/contacts/" + e.data.id + "?format=json")
                        .then(function (response) {
                            if (response.data.status == "NotExisted") {
                                // on failure
                                e.error("XHR response", "111", "Contact name " + e.data.name + " does not exists");
                            }
                            else {
                                // on success
                                e.success();
                                $scope.totalRecord -= 1;
                            }
                        });
                }
            },
            error: function (e) {
                // handle data operation error
                alert("Status: " + e.status + "; Error message: " + e.errorThrown);
            },
            pageSize: 5,
            batch: false,
            schema: {
                total: function () {
                    return $scope.totalRecord;
                },
                model: {
                    id: "id",
                    fields: {
                        id: { editable: false, nullable: true },
                        name: { validation: { required: true, existed: true }, nullable: false },
                        phone_number: { validation: { required: true } }
                    }
                }
            },
            serverPaging: true
        });

        //Config data source phone number
        var getDataSourcePhoneNumbersByContactID = function (contactID) {
            return new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        // server paging
                        $http.get("/contactsDetail/phoneNumber/" + contactID + "?format=json")
                            .then(function (response) {
                                // on success
                                e.success(response.data);
                            });
                    },
                    create: function (e) {
                        //create model data update
                        var model = { "number": e.data.number, "type": e.data.type, "contactID": contactID };
                        $http.post("/contactsDetail/phoneNumber/", model)
                            .then(function (response) {
                                if (response.data.status == "Existed") {
                                    // on failure
                                    e.error("XHR response", "111", "Phone number " + e.data.number + " already exists");
                                }
                                else {
                                    // on success
                                    e.success();
                                }
                            });
                    },
                    update: function (e) {
                        //create model data update
                        var model = { "number": e.data.number, "type": e.data.type };
                        //call api update
                        $http.put("/contactsDetail/phoneNumber/" + e.data.id + "?format=json", model)
                            .then(function (response) {
                                if (response.data.status == "NotExisted") {
                                    // on failure
                                    e.error("XHR response", "111", "PhoneNumberID " + e.data.id + " does not exists");
                                }
                                else {
                                    // on success
                                    e.success();
                                }
                            });
                    },
                    destroy: function (e) {
                        //call api delete
                        $http.delete("/contactsDetail/phoneNumber/" + e.data.id + "?format=json")
                            .then(function (response) {
                                if (response.data.status == "NotExisted") {
                                    // on failure
                                    e.error("XHR response", "111", "Contact name " + e.data.id + " does not exists");
                                }
                                else {
                                    // on success
                                    e.success();
                                }
                            });
                    }
                },
                error: function (e) {
                    // handle data operation error
                    alert("Status: " + e.status + "; Error message: " + e.errorThrown);
                },
                batch: false,
                schema: {
                    model: {
                        id: "id",
                        fields: {
                            id: { editable: false, nullable: false },
                            number: { validation: { required: true, existed: true }, nullable: false },
                            type: { type: "combobox", validation: { required: true, existed: true }, nullable: false },
                            contact_id: { editable: false, nullable: false },
                        }
                    }
                },
                serverPaging: false
            });
        }

        //Custom drop down category phone number type
        function phoneNumberTypeDropDownEditor(container, options) {
            $('<input required name="' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    autoBind: false,
                    dataSource: ["Mobile", "Home", "Work"]
                });
        }
    });
})();

