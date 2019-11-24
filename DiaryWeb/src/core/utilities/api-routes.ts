import { environment } from 'src/environments/environment';

export const ApiRoutes = {
    authenticate: `${environment.identityServerUrl}/accounts/authenticate`,
    refreshToken: ``
};
