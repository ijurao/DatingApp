import { Photo } from './Photo';

export interface User {

    id: number;
    userName: string;
    knownAs: string;
    gender: string;
    createDdate: Date;
    lastAcces: string;
    mainPhotoUrl: string;
    city: string;
    country: string;
    interests?: string;
    intro?: string;
    lookingFor?: string;
    photos?: Photo[];
    age: number;

}
