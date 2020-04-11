import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../service/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation } from '../Shared/enum';
import { Observable } from 'rxjs/Rx';
import { Global } from '../shared/global';

import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { ISoccerMatch } from '../models/soccermatch';


@Component({
    templateUrl: 'app/components/soccermatch.component.html'
})
export class SoccerMatchComponent implements OnInit {
    @ViewChild('modal') modal: ModalComponent;
    soccermatches: ISoccerMatch[];
    soccermatch: ISoccerMatch;
    msg: string;
    indLoading: boolean = false;
    dbops: DBOperation;
    modalTitle: string;
    modalBtnTitle: string;
    modalForm: FormGroup;
    constructor(private fb: FormBuilder, private _userService: UserService) { }
    ngOnInit(): void {

        this.modalForm = this.fb.group({
            IDMatch: [{ value: '', disable: true }],
            HomeTeam: ['', Validators.required],
            GuestTeam: ['', Validators.required],
            Stadium: ['', Validators.required],
            City: ['', Validators.required],
            IdLeague: ['', Validators.required]
        });

        this.LoadSoccerMatchs();
    }

    LoadSoccerMatchs(): void {
        this.indLoading = true;
        this._userService.get(Global.BASE_USER_ENDPOINTS)
            .subscribe(soccermatchs => { this.soccermatches = soccermatchs; this.indLoading = false; },
                error => this.msg = <any>error);
    }
    addSoccerMatch() {
        this.dbops = DBOperation.create;
        this.SetControlsState(true);
        this.modalTitle = "Add new soccermatch";
        this.modalBtnTitle = "Add";
        this.modalForm.reset();
        this.modal.open();
    }
    editSoccerMatch(id: string) {
        this.dbops = DBOperation.update;
        this.SetControlsState(true);
        this.modalTitle = "Edit soccermatch";
        this.modalBtnTitle = "Edit";
        this.soccermatch = this.soccermatches.filter(x => x.IDMatch == id)[0];
        this.modalForm.setValue(this.soccermatch);
        this.modal.open();
    }
    deleteSoccerMatch(id: string) {
        this.dbops = DBOperation.delete;
        this.SetControlsState(false);
        this.modalTitle = "Confirm to Delete?";
        this.modalBtnTitle = "Delete";
        this.soccermatch = this.soccermatches.filter(x => x.IDMatch == id)[0];
        this.modalForm.setValue(this.soccermatch);
        this.modal.open();
    }

    SetControlsState(isEnable: boolean) {
        isEnable ? this.modalForm.enable() : this.modalForm.disable();
    }

    onSubmit(formData: any) {
        this.msg = "";
        switch (this.dbops) {
            case DBOperation.create:
                this._userService.post(Global.BASE_USER_ENDPOINTS, formData._value).subscribe(
                    data => {
                        if (data == 1) {
                            this.msg = "Data successfullly added!";
                            this.LoadSoccerMatchs();
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
                this._userService.put(Global.BASE_USER_ENDPOINTS, formData._value.IDMatch,
                    formData._value).subscribe(
                        data => {
                            if (data == 1) //Success
                            {
                                this.msg = "Data successfully updated.";
                                this.LoadSoccerMatchs();
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
                this._userService.delete(Global.BASE_USER_ENDPOINTS, formData._value.IDMatch).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully deleted.";
                            this.LoadSoccerMatchs();
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