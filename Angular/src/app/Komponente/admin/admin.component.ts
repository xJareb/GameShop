import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {ListOfAllUsers, User} from "../../Servis/AdminService/list-of-all-users";
import {NgForOf, NgIf} from "@angular/common";
import {Admin, AdminData} from "../../Servis/AdminService/admin-data";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    RouterLink,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent implements OnInit{

    public selectedFile:File |null = null;

    listOfUsers:User[] = [];
    blackList:User[] = [];
    adminData: Admin[] = [];

    constructor(public httpClient:HttpClient,public authService:MyAuthServiceService,public router:Router) {
    }
    ngOnInit(): void {
      if(!this.authService.isAdmin()){
        this.router.navigate(["/"])
      }else{
        this.listAllUsers();
        this.listBlacklist();
        this.listAdmin();
      }

    }
    listAdmin(){
      let id = this.handleUser().authenticationToken.userAccount.id;
      let url = MojConfig.adresa_servera + `/GetAdmin?ID=${id}`;

      this.httpClient.get<AdminData>(url,{
        headers:{
          "my-auth-token":this.authService.returnToken()
        }
      }).subscribe((x:AdminData) => {
        this.adminData = x.admin;
      })
    }
    listAllUsers(){
        let url = MojConfig.adresa_servera + `/UsersGet?isBlackList=false`;

        this.httpClient.get<ListOfAllUsers>(url,{
          headers:{
            "my-auth-token":this.authService.returnToken()
          }
        }).subscribe((x:ListOfAllUsers)=>{
              this.listOfUsers = x.users;
        })
    }
    listBlacklist(){
      let url = MojConfig.adresa_servera + `/UsersGet?isBlackList=true`;

      this.httpClient.get<ListOfAllUsers>(url,{
        headers:{
          "my-auth-token":this.authService.returnToken()
        }
      }).subscribe((x:ListOfAllUsers)=>{
        this.blackList = x.users;
      })
    }
    deleteUser(lk: User) {
    let id = lk.id;

    let url = MojConfig.adresa_servera + `/UserDelete?ID=${id}&isBlackList=false`;
    this.httpClient.put(url,{},{
      headers:{
        "my-auth-token":this.authService.returnToken()
      }
    }).subscribe({
      next:(response)=> {
        this.listAllUsers();
      }})
  }

    blacklistUser(lk: User) {
    let id = lk.id;
    let url = MojConfig.adresa_servera + `/UserDelete?ID=${id}&isBlackList=true`;

    this.httpClient.put(url,{},{
      headers:{
        "my-auth-token":this.authService.returnToken()
      }
    }).subscribe({
      next:(response)=>{
        this.listAllUsers();
        this.listBlacklist();
      }})
    }

  onSubmit() {
    if (!this.selectedFile) {
      alert('Please choose a file.');
      return;
    }
    const formData = new FormData();
    formData.append("photo", this.selectedFile);

    let photoUrl = MojConfig.adresa_servera + `/UserPhoto`;

    this.httpClient.put(photoUrl,formData).subscribe({
      next:(response) => {
        this.listAdmin();
      }
    })
  }
  onFileSelected($event: Event) {
    const target = $event.target as HTMLInputElement;
    const file = target.files?.[0];
    if (file) {
      this.selectedFile = file;
    }
  }
  handleUser(){
    return JSON.parse(window.localStorage.getItem("korisnik")??"");
  }
}
