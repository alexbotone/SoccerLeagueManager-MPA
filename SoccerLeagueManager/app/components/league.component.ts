import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../service/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation } from '../Shared/enum';
import { Observable } from 'rxjs/Rx';
import { Global } from '../shared/global';
import { ILeague } from '../models/league';

import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';


@Component({
    templateUrl: 'app/components/league.component.html'
})
export class LeagueComponent implements OnInit {
    @ViewChild('modal') modal: ModalComponent;
    leagues: ILeague[];
    league: ILeague;
    msg: string;
    indLoading: boolean = false;
    dbops: DBOperation;
    modalTitle: string;
    modalBtnTitle: string;
    modalForm: FormGroup;
    constructor(private fb: FormBuilder, private _userService: UserService) { }
    ngOnInit(): void {

        this.modalForm = this.fb.group({
            IDLeague: [{ value: '', disable: true }],
            NameLeague: ['', Validators.required],
            Country: ['', Validators.required],
            NumberOfTeams: ['', Validators.required],
            Sponsor: ['', Validators.required]
           
        });

        this.LoadLeagues();
    }

    LoadLeagues(): void {
        this.indLoading = true;
        this._userService.get(Global.BASE_USER_ENDPOINTL)
            .subscribe(leagues => { this.leagues = leagues; this.indLoading = false; },
                error => this.msg = <any>error);
    }
    addLeague() {
        this.dbops = DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Add new league";
        this.modalBtnTitle = "Add";
        this.modalForm.reset();
        this.modal.open();
    }
    editLeague(id: string) {
        this.dbops = DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Edit league";
        this.modalBtnTitle = "Edit";
        this.league = this.leagues.filter(x => x.IDLeague == id)[0];
        this.modalForm.setValue(this.league);
        this.modal.open();
    }
    deleteLeague(id: string) {
        this.dbops = DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Confirm to Delete?";
        this.modalBtnTitle = "Delete";
        this.league = this.leagues.filter(x => x.IDLeague == id)[0];
        this.modalForm.setValue(this.league);
        this.modal.open();
    }

    SetControlsState(isEnable: boolean) {
        isEnable ? this.modalForm.enable() : this.modalForm.disable();
    }

    onSubmit(formData: any) {
        this.msg = "";
        switch (this.dbops) {
            case DBOperation.create:
                this._userService.post(Global.BASE_USER_ENDPOINTL, formData._value).subscribe(
                    data => {
                        if (data == 1) {
                            this.msg = "Data successfullly added!";
                            this.LoadLeagues();
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
                this._userService.put(Global.BASE_USER_ENDPOINTL, formData._value.IDLeague,formData._value).subscribe(
                        data => {
                            if (data == 1) //Success
                            {
                                this.msg = "Data successfully updated.";
                                this.LoadLeagues();
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
                this._userService.delete(Global.BASE_USER_ENDPOINTL, formData._value.IDLeague).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully deleted.";
                            this.LoadLeagues();
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