import {NgModule} from '@angular/core';
import {HttpClientModule, HttpClient} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {BrowserModule} from '@angular/platform-browser';
import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
// import { RouterModule } from '@angular/router';
// import { routes } from './app.router';

import{MaterialModule} from './shared/material.module';
import{FlexLayoutModule} from '@angular/flex-layout';
import { MatStepperModule } from '@angular/material/stepper';
import { MatInputModule } from '@angular/material/input';
import { RouterModule } from '@angular/router';
import { routes } from './app.router';
import {NgxMaskModule} from 'ngx-mask'
import { BotDetectCaptchaModule } from 'angular-captcha';

import {TranslateModule, TranslateLoader} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import { DateFormat } from './date-format';
import {
  MatDatepickerModule,
  MatNativeDateModule,
  DateAdapter
} from "@angular/material";
import { HomeComponent } from './components/home/home.component';
import { DialogComponent } from './components/dialog/dialog.component';
import { AgreeDiaglogComponent } from './components/agree-diaglog/agree-diaglog.component';
import { VerifydialogComponent } from './components/verifydialog/verifydialog.component';
import { FormContainerComponent } from './components/form-container/form-container.component';
import { HeaderComponent } from './components/header/header.component';
import { EmailService } from './services/emailservice/email.service';
import { RoboschienSpainService } from './services/roboschienSpainservice/roboschienSpain.service';

import { FooterComponent } from './components/footer/footer.component';
import { ReCaptchaValidateComponent } from './components/recaptcha-validate/recaptcha-validate.component';
import { Ng2ImgMaxModule } from 'ng2-img-max';
import { ImprintsComponent } from './components/imprints/imprints.component';
import { MatTabsModule } from '@angular/material';
import { HelpContentComponent } from './components/help-content/help-content.component';
import { HelpCertificateComponent } from './components/help-certificate/help-certificate.component';
import { CountrySelectionComponent } from './components/country-selection/country-selection.component';
import { FossLicenseComponent } from './components/foss-license/foss-license.component';
import { InputRestrictionDirectiveDirective } from './directive/input-restriction-directive.directive';


// AoT requires an exported function for factories
export function HttpLoaderFactory(httpClient: HttpClient) {
  return new TranslateHttpLoader(httpClient);
}


@NgModule({
  declarations: [
    AppComponent,
    FormContainerComponent,
    HeaderComponent,
    DialogComponent,
    AgreeDiaglogComponent,
    VerifydialogComponent,
    HomeComponent,

    FooterComponent,
    ReCaptchaValidateComponent,
    ImprintsComponent,
    HelpContentComponent,
    HelpCertificateComponent,
    CountrySelectionComponent,
    FossLicenseComponent,
    InputRestrictionDirectiveDirective
  ],
  entryComponents: [DialogComponent, AgreeDiaglogComponent,  ReCaptchaValidateComponent, HelpContentComponent, HelpCertificateComponent,
    CountrySelectionComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    FlexLayoutModule,
    HttpClientModule,
    NgxMaskModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    MaterialModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatInputModule,
    RouterModule.forRoot(routes),
    MatDatepickerModule,
    MatNativeDateModule,
    Ng2ImgMaxModule,
    MatTabsModule
  ],
  exports:[
    DialogComponent,
    MatStepperModule,
    MatInputModule

  ],
  providers: [EmailService, RoboschienSpainService],
  bootstrap: [AppComponent],

})
export class AppModule {
  constructor(private dateAdapter: DateAdapter<Date>) {
    // Date picker
    let lang = localStorage.getItem("lang");
    lang == null ? "en" : lang;
    dateAdapter.setLocale(lang);
  }
}
