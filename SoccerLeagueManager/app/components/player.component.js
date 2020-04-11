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
var PlayerComponent = /** @class */ (function () {
    function PlayerComponent(fb, _userService) {
        this.fb = fb;
        this._userService = _userService;
        this.indLoading = false;
    }
    PlayerComponent.prototype.ngOnInit = function () {
        this.modalForm = this.fb.group({
            IDPlayer: [{ value: '', disable: true }],
            Name: ['', forms_1.Validators.required],
            Surname: ['', forms_1.Validators.required],
            Position: ['', forms_1.Validators.required],
            Team: ['', forms_1.Validators.required],
            Value_EUR: ['', forms_1.Validators.required],
            IdTeam: ['', forms_1.Validators.required]
        });
        this.LoadPlayers();
    };
    PlayerComponent.prototype.LoadPlayers = function () {
        var _this = this;
        this.indLoading = true;
        this._userService.get(global_1.Global.BASE_USER_ENDPOINT)
            .subscribe(function (players) { _this.players = players; _this.indLoading = false; }, function (error) { return _this.msg = error; });
    };
    PlayerComponent.prototype.addPlayer = function () {
        this.dbops = enum_1.DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Add new player";
        this.modalBtnTitle = "Add";
        this.modalForm.reset();
        this.modal.open();
    };
    PlayerComponent.prototype.editPlayer = function (id) {
        this.dbops = enum_1.DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Edit player";
        this.modalBtnTitle = "Edit";
        this.player = this.players.filter(function (x) { return x.IDPlayer == id; })[0];
        this.modalForm.setValue(this.player);
        this.modal.open();
    };
    PlayerComponent.prototype.deletePlayer = function (id) {
        this.dbops = enum_1.DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Confirm to Delete?";
        this.modalBtnTitle = "Delete";
        this.player = this.players.filter(function (x) { return x.IDPlayer == id; })[0];
        this.modalForm.setValue(this.player);
        this.modal.open();
    };
    PlayerComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.modalForm.enable() : this.modalForm.disable();
    };
    PlayerComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                this._userService.post(global_1.Global.BASE_USER_ENDPOINT, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfullly added!";
                        _this.LoadPlayers();
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
                this._userService.put(global_1.Global.BASE_USER_ENDPOINT, formData._value.IDPlayer, formData._value).subscribe(function (data) {
                    if (data == 1) //Success
                     {
                        _this.msg = "Data successfully updated.";
                        _this.LoadPlayers();
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
                this._userService.delete(global_1.Global.BASE_USER_ENDPOINT, formData._value.IDPlayer).subscribe(function (data) {
                    if (data == 1) //Success
                     {
                        _this.msg = "Data successfully deleted.";
                        _this.LoadPlayers();
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
    ], PlayerComponent.prototype, "modal", void 0);
    PlayerComponent = __decorate([
        core_1.Component({
            templateUrl: 'app/components/player.component.html'
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, user_service_1.UserService])
    ], PlayerComponent);
    return PlayerComponent;
}());
exports.PlayerComponent = PlayerComponent;
//# sourceMappingURL=player.component.js.map