import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../service/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation } from '../Shared/enum';
import { Observable } from 'rxjs/Rx';
import { Global } from '../shared/global';

import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { ITeam } from '../models/team';


@Component({
    templateUrl: 'app/components/team.component.html'
})
export class TeamComponent implements OnInit {
    @ViewChild('modal') modal: ModalComponent;
    teams: ITeam[];
    team: ITeam;
    msg: string;
    indLoading: boolean = false;
    dbops: DBOperation;
    modalTitle: string;
    modalBtnTitle: string;
    modalForm: FormGroup;
    constructor(private fb: FormBuilder, private _userService: UserService) { }
    ngOnInit(): void {

        this.modalForm = this.fb.group({
            IDTeam: [{ value: '', disable: true }],
            Name: ['', Validators.required],
            City: ['', Validators.required],
            Email: ['', Validators.required],
            IdLeague: ['', Validators.required]
           
        });

        this.LoadTeams();
    }

    LoadTeams(): void {
        this.indLoading = true;
        this._userService.get(Global.BASE_USER_ENDPOINTT)
            .subscribe(teams => { this.teams = teams; this.indLoading = false; },
                error => this.msg = <any>error);
    }
    addTeam() {
        this.dbops = DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Add new team";
        this.modalBtnTitle = "Add";
        this.modalForm.reset();
        this.modal.open();
    }
    editTeam(id: string) {
        this.dbops = DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Edit team";
        this.modalBtnTitle = "Edit";
        this.team = this.teams.filter(x => x.IDTeam == id)[0];
        this.modalForm.setValue(this.team);
        this.modal.open();
    }
    deleteTeam(id: string) {
        this.dbops = DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Confirm to Delete?";
        this.modalBtnTitle = "Delete";
        this.team = this.teams.filter(x => x.IDTeam == id)[0];
        this.modalForm.setValue(this.team);
        this.modal.open();
    }

    SetControlsState(isEnable: boolean) {
        isEnable ? this.modalForm.enable() : this.modalForm.disable();
    }

    onSubmit(formData: any) {
        this.msg = "";
        switch (this.dbops) {
            case DBOperation.create:
                this._userService.post(Global.BASE_USER_ENDPOINTT, formData._value).subscribe(
                    data => {
                        if (data == 1) {
                            this.msg = "Data successfullly added!";
                            this.LoadTeams();
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
                this._userService.put(Global.BASE_USER_ENDPOINTT, formData._value.IDTeam,
                    formData._value).subscribe(
                        data => {
                            if (data == 1) //Success
                            {
                                this.msg = "Data successfully updated.";
                                this.LoadTeams();
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
                this._userService.delete(Global.BASE_USER_ENDPOINTT, formData._value.IDTeam).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully deleted.";
                            this.LoadTeams();
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