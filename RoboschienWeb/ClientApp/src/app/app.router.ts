

import { Route, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, ActivatedRoute } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { ImprintsComponent } from './components/imprints/imprints.component';

export const routes: Route[] = [
	{
		// tslint:disable-next-line:indent
		path: '',
		redirectTo: '/home',
		pathMatch: 'full',
	},
	{
		// tslint:disable-next-line:indent
      path: 'home',
      component: HomeComponent
  },
  {
     path: 'imprints', component: ImprintsComponent 
  }
];
