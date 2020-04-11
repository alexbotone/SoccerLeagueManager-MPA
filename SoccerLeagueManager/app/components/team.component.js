"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var user_service_1 = require("../service/user.service");
var forms_1 = require("@angular/forms");
var enum_1 = require("../Shared/enum");
var global_1 = require("../shared/global");
var ng2_bs3_modal_1 = require("ng2-bs3-modal/ng2-bs3-modal");
var TeamComponent = /** @class */ (function () {
    function TeamComponent(fb, _userService) {
        this.fb = fb;
        this._userService = _userService;
        this.indLoading = false;
    }
    TeamComponent.prototype.ngOnInit = function () {
        this.modalForm = this.fb.group({
            IDTeam: [{ value: '', disable: true }],
            Name: ['', forms_1.Validators.required],
            City: ['', forms_1.Validators.required],
            Email: ['', forms_1.Validators.required],
            IdLeague: ['', forms_1.Validators.required]
        });
        this.LoadTeams();
    };
    TeamComponent.prototype.LoadTeams = function () {
        var _this = this;
        this.indLoading = true;
        this._userService.get(global_1.Global.BASE_USER_ENDPOINTT)
            .subscribe(function (teams) { _this.teams = teams; _this.indLoading = false; }, function (error) { return _this.msg = error; });
    };
    TeamComponent.prototype.addTeam = function () {
        this.dbops = enum_1.DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Add new team";
        this.modalBtnTitle = "Add";
        this.modalForm.reset();
        this.modal.open();
    };
    TeamComponent.prototype.editTeam = function (id) {
        this.dbops = enum_1.DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Edit team";
        this.modalBtnTitle = "Edit";
        this.team = this.teams.filter(function (x) { return x.IDTeam == id; })[0];
        this.modalForm.setValue(this.team);
        this.modal.open();
    };
    TeamComponent.prototype.deleteTeam = function (id) {
        this.dbops = enum_1.DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Confirm to Delete?";
        this.modalBtnTitle = "Delete";
        this.team = this.teams.filter(function (x) { return x.IDTeam == id; })[0];
        this.modalForm.setValue(this.team);
        this.modal.open();
    };
    TeamComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.modalForm.enable() : this.modalForm.disable();
    };
    TeamComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                this._userService.post(global_1.Global.BASE_USER_ENDPOINTT, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfullly added!";
                        _this.LoadTeams();
                    }
                    else {
                        _this.msg = "There is some issue in saving records.";
                    }
                    _this.modal.dismiss();
                }, function (error) {
                    _this.msg = error;
                });
                break;
            case enum_1.DBOperation.update:
                this._userService.put(global_1.Global.BASE_USER_ENDPOINTT, formData._value.IDTeam, formData._value).subscribe(function (data) {
                    if (data == 1) //Success
                     {
                        _this.msg = "Data successfully updated.";
                        _this.LoadTeams();
                    }
                    else {
                        _this.msg = "There is some issue in saving records!";
                    }
                    _this.modal.dismiss();
                }, function (error) {
                    _this.msg = error;
                });
                break;
            case enum_1.DBOperation.delete:
                this._userService.delete(global_1.Global.BASE_USER_ENDPOINTT, formData._value.IDTeam).subscribe(function (data) {
                    if (data == 1) //Success
                     {
                        _this.msg = "Data successfully deleted.";
                        _this.LoadTeams();
                    }
                    else {
                        _this.msg = "There is some issue in saving records!";
                    }
                    _this.modal.dismiss();
                }, function (error) {
                    _this.msg = error;
                });
                break;
        }
    };
    __decorate([
        core_1.ViewChild('modal'),
        __metadata("design:type", ng2_bs3_modal_1.ModalComponent)
    ], TeamComponent.prototype, "modal", void 0);
    TeamComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/components/team.component.html'
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, user_service_1.UserService])
    ], TeamComponent);
    return TeamComponent;
}());
exports.TeamComponent = TeamComponent;
//# sourceMappingURL=team.component.js.map