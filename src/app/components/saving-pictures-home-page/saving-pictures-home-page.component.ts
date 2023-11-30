import { ChangeDetectorRef, Component, ComponentFactoryResolver, ElementRef, OnChanges, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SavingPicturesService } from '../../services/saving-pictures.service';
import { NewPictureComponent } from '../new-picture/new-picture.component';
import { CollectionInfo } from '../../models/collectionInfo';
import { Picture } from '../../models/picture';
import { NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-saving-pictures-home-page',
  templateUrl: './saving-pictures-home-page.component.html',
  styleUrls: ['./saving-pictures-home-page.component.css']
})
export class SavingPicturesHomePageComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  collectionName: boolean = false;
  collectionSymbolizationRequired: boolean = false;
  collectionSymbolizationRequiredControl!: FormControl;
  collectionInfo!: CollectionInfo;
  numOfPictures: number = 0;
  required: boolean = false;
  notValidSymbolization: boolean = false;
  picturesToSend: Picture[] = [];
  // deleteButton:boolean=false;
  photoCards: any[] = [];
  @ViewChild('photoCardContainer', { read: ViewContainerRef }) container: ViewContainerRef | undefined;

  ngOnInit(): void {
    this.initForm()
    this.collectionSymbolizationRequiredControl = this.form.get('collectionSymbolizationRequired') as FormControl;
  }



  constructor(
    private spService: SavingPicturesService,
    private componentFactoryResolver: ComponentFactoryResolver,
    private _snackBar: MatSnackBar
  ) { }

  initForm() {
    this.form = new FormGroup({
      collectionSymbolization: new FormControl('', [Validators.required]),
      collectionTitle: new FormControl("", [Validators.required]),
      collectionSymbolizationRequired: new FormControl(false),
      notValidSymbolization: new FormControl(false)
    })
  }
  updateCollectionName() {
    if (this.form.value.collectionSymbolization) {
      var data = this.spService.getCollectionName(this.form.value.collectionSymbolization);
      data.subscribe((res: any) => {
        if (res) {
          this.form.patchValue({ collectionTitle: res.title })
          this.collectionInfo = res;
          this.collectionName = true;
        }
        else {
          this.form.patchValue({ notValidSymbolization: true });
        }
      })
    }
    else {
      this.form.patchValue({ collectionSymbolizationRequired: true });
    }
  }

  isCollectionSymbolizationRequired(): boolean {
    return this.collectionSymbolizationRequiredControl.value;
  }

  addNewPicture() {
    if (!this.container) {
      return;
    }
    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(NewPictureComponent);
    const componentRef = this.container.createComponent(componentFactory);
    const photoCardComponent = componentRef.instance;
    if (this.photoCards.length == 0) {
      this.numOfPictures = this.collectionInfo.numOfPictures;
      photoCardComponent.numOfPictures = this.collectionInfo.numOfPictures;
    }
    else {
      this.numOfPictures++;
      photoCardComponent.numOfPictures = this.numOfPictures;
    }
    photoCardComponent.collectionSymbolization = this.form.value.collectionSymbolization;
    photoCardComponent.title = this.form.value.collectionTitle;

    this.photoCards.push(photoCardComponent);
  }

  savePictures() {
    this.initDataToSend()
    this.spService.addNewPictures(this.picturesToSend).subscribe((res: string) => {
      this.openSnackBar();
      this.picturesToSend = [];
      this.form.reset();
      this.numOfPictures = 0;
      this.photoCards = [];
      this.collectionName = false;
    })
  }

  initDataToSend() {
    this.photoCards.forEach(e => {
      const picture: Picture = {
        pictureName: e.numOfPictures.toString(),
        collectionTitle: e.title,
        collectionSymbolizationID: e.collectionSymbolization,
        pictureUrl: '',
        backName: e.backImg ? e.form.value.backPictureName : null,
        backUrl: e.backImg ? e.form.value.backPictureUrl : null
      }
      this.picturesToSend.push(picture);
    })
  }
  openSnackBar() {
    this._snackBar.openFromComponent(SnackBarComponent, {
      duration: 5 * 1000,
    });

  }


  deletePicture(index: number) {
    if (index >= 0 && index < this.photoCards.length) {
      this.photoCards.pop();
      this.form.patchValue({ collectionSymbolizationRequired: this.required });
      if (this.container) {
        this.container.remove(index);
        this.numOfPictures--;
      }
    }
  }

}

@Component({
  selector: 'snack-bar-component-example-snack',
  template: `<span class="example-pizza-party">
 photos added successfully🥳🥳
</span>`,
  styles: [
    `
    .example-pizza-party {
      color: hotblue;
    }
  `,
  ],
  standalone: true,
})
export class SnackBarComponent { }
