import { environment } from 'src/environments/environment';

export const ApiRoutes = {
    authenticate: `${environment.identityServerUrl}/accounts/authenticate`,
    refreshToken: ``,
    resetPassword: ``,
    userProfile: `${environment.identityServerUrl}/users`,

    studentResults: `${environment.diaryApi}/diaries`
};
