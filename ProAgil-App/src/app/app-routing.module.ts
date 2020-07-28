import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EventosComponent } from './eventos/eventos.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { DashboardsComponent } from './dashboards/dashboards.component';
import { ContatosComponent } from './contatos/contatos.component';


const routes: Routes = [
  { path: 'eventos', component: EventosComponent },
  { path: 'palestrantes', component: PalestrantesComponent },
  { path: 'dashboards', component: DashboardsComponent },
  { path: 'contatos', component: ContatosComponent },
  { path: '', redirectTo: 'dashboards', pathMatch: 'full' },
  { path: '**', redirectTo: 'dashboards', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
