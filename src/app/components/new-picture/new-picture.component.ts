import { Component, Input, OnChanges } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-new-picture',
  templateUrl: './new-picture.component.html',
  styleUrls: ['./new-picture.component.css']
})
export class NewPictureComponent{
  @Input()
  collectionSymbolization!: string;
  @Input()
  title!: string;
  @Input()
  numOfPictures!: number;
 
  form: FormGroup = new FormGroup({});


  ngOnInit(): void {
    this.initForm()
    this.initPhotoName();

  }

  initForm() {
    this.form = new FormGroup({
      pictureName: new FormControl(),
      pictureUrl: new FormControl(""),
      backImg: new FormControl(false),
      backPictureName: new FormControl(),
      backPictureUrl: new FormControl()
    })
    this.onValueChange();

  }

  getBackImgControl(): FormControl {
    return this.form.get('backImg') as FormControl;
  }

  initPhotoName() {
    this.numOfPictures += 1;
    this.form.patchValue({ pictureName: this.numOfPictures });
    this.form.patchValue({ pictureUrl: "images\\" + this.collectionSymbolization + "\\" + this.numOfPictures + ".jpg" })

  }
  onValueChange() {
    this.form.get('backImg')?.valueChanges.subscribe(res => {
      if (this.backImg == true) {
        this.form.patchValue({ backPictureName: this.pictureName + "_xx" });
        this.form.patchValue({ backPictureUrl: this.pictureUrl + "_xx" });
      }
    });
  }
  deletePicture(){

  }

  get backImg() { return this.form.get('backImg')?.value };
  get pictureName() { return this.form.get('pictureName')?.value };
  get pictureUrl() { return this.form.get('pictureUrl')?.value };

}
