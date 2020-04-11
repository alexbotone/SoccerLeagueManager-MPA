import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../service/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation } from '../Shared/enum';
import { Observable } from 'rxjs/Rx';
import { Global } from '../shared/global';

import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { IPlayer } from '../models/player';


@Component({
    templateUrl: 'app/components/player.component.html'
})
export class PlayerComponent implements OnInit {
    @ViewChild('modal') modal: ModalComponent;
    players: IPlayer[];
    player: IPlayer;
    msg: string;
    indLoading: boolean = false;
    dbops: DBOperation;
    modalTitle: string;
    modalBtnTitle: string;
    modalForm: FormGroup;
    constructor(private fb: FormBuilder, private _userService: UserService) { }
    ngOnInit(): void {

        this.modalForm = this.fb.group({
            IDPlayer: [{ value: '', disable: true }],
            Name: ['', Validators.required],
            Surname: ['', Validators.required],
            Position: ['', Validators.required],
            Team: ['', Validators.required],
            Value_EUR: ['', Validators.required],
            IdTeam: ['', Validators.required]
        });

        this.LoadPlayers();
    }

    LoadPlayers(): void {
        this.indLoading = true;
        this._userService.get(Global.BASE_USER_ENDPOINT)
            .subscribe(players => { this.players = players; this.indLoading = false; },
                error => this.msg = <any>error);
    }
    addPlayer() {
        this.dbops = DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Add new player";
        this.modalBtnTitle = "Add";
        this.modalForm.reset();
        this.modal.open();
    }
    editPlayer(id: string) {
        this.dbops = DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Edit player";
        this.modalBtnTitle = "Edit";
        this.player = this.players.filter(x => x.IDPlayer == id)[0];
        this.modalForm.setValue(this.player);
        this.modal.open();
    }
    deletePlayer(id: string) {
        this.dbops = DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Confirm to Delete?";
        this.modalBtnTitle = "Delete";
        this.player = this.players.filter(x => x.IDPlayer == id)[0];
        this.modalForm.setValue(this.player);
        this.modal.open();
    }

    SetControlsState(isEnable: boolean) {
        isEnable ? this.modalForm.enable() : this.modalForm.disable();
    }

    onSubmit(formData: any) {
        this.msg = "";
        switch (this.dbops) {
            case DBOperation.create:
                this._userService.post(Global.BASE_USER_ENDPOINT, formData._value).subscribe(
                    data => {
                        if (data == 1) {
                            this.msg = "Data successfullly added!";
                            this.LoadPlayers();
                        }
                        else {
                            this.msg = "There is some issue in saving records.";
                        }
                        this.modal.dismiss();
                    },
                    error => {
                        this.msg = error;
                    }
                );
                break;
            case DBOperation.update:
                this._userService.put(Global.BASE_USER_ENDPOINT, formData._value.IDPlayer,
                    formData._value).subscribe(
                        data => {
                            if (data == 1) //Success
                            {
                                this.msg = "Data successfully updated.";
                                this.LoadPlayers();
                            }
                            else {
                                this.msg = "There is some issue in saving records!";
                            }
                            this.modal.dismiss();
                        },
                        error => {
                            this.msg = error;
                        }
                    );
                break;
            case DBOperation.delete:
                this._userService.delete(Global.BASE_USER_ENDPOINT, formData._value.IDPlayer).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully deleted.";
                            this.LoadPlayers();
                        }
                        else {
                            this.msg = "There is some issue in saving records!";
                        }
                        this.modal.dismiss();
                    },
                    error => {
                        this.msg = error;
                    }
                );
                break;
        }
    }
}