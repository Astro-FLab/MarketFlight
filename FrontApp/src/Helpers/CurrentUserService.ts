
export class CurrentUserService {
    private constructor() { }
    private _currentUserId: number;
    private static instance: CurrentUserService;


    get currentUserId(): number {
        return this._currentUserId;
    }
    set currentUserId(value: number) {
        this._currentUserId = value;
    }

    static getInstance(): CurrentUserService {
        if (!CurrentUserService.instance) {
            CurrentUserService.instance = new CurrentUserService();
        }

        return CurrentUserService.instance;
    }
}