import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AdminModule } from './admin.module';

const platform = platformBrowserDynamic();
platform.bootstrapModule(AdminModule);