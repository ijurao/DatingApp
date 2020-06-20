import { Photo } from './Photo';

export interface User {

    id: number;
    userName: string;
    knownAs: string;
    gender: string;
    createdDate: Date;
    lastAcces: string;
    mainPhotoUrl: string;
    city: string;
    country: string;
    interests?: string;
    intro?: string;
    lookingFor?: string;
    photos?: Photo[];
    age: number;
    passwordRepeat : string;

}
