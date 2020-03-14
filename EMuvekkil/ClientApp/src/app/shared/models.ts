

export interface Masraf {
    date: string;
    user: string;
    amount: number;
    description: string;
}

export interface Document {
    date: string;
    user: string;
    name: string;
    description: string;
}

export interface Message {
    date: string;
    user: string;
    message: string;
}

export interface LoginViewModel {
    email: string;
    password: string;
}

export interface UserInfoModel {
    id: string;
    identityNumber: string;
    email: string;
    nameSurname: string;
    role: string;
}

export interface LoginResponse {
    isError: boolean;
    message: string;
}

export interface UserModel {
    id: string;
    identityNumber: string;
    nameSurname: string;
    email: string;
    companyName: string;
}

export interface RegisterViewModel {
    id: string;
    email: string;
    identityNumber: string;
    nameSurname: string;
    password: string;
    companyId: number;
}


export interface DavaViewModel {
    id: number;
    name: string;
    avukatName: string;
    muvekkilName: string;
    muvekkilId: string;
    avukatId: string;
    davaStateText: string;
    davaStateId: number;
}

export interface MasrafViewModel {
    id: number;
    description: string;
    amount: number;
    ownerUserName: string;
    ownerName: string
    davaId: number;
    date: Date;
}

export interface DocumentViewModel {
    id: number;
    ownerName: string;
    ownerUserName: string;
    date: Date;
    fileName: string;
    description: string;
    davaName: string;
    davaId: number;
    file: File;
    isActive: boolean;
}

export interface MessageViewModel {
    id: number;
    date: Date;
    ownerName: string
    ownerUserName: string;
    text: string;
    davaName: string;
    davaId: number;
    isActive: boolean;
}

export interface CompanyViewModel {
    id: number;
    name: string;
    dependecies: number;
}

export interface DavaStateViewModel {
    id: number;
    text: string;

}

export interface ReportViewModel {
    startDate: Date;
    endDate: Date;
    avukatId: string;
    muvekkilId: string;
    davaId: number;
    dateDisabled: boolean;
}

export interface ReportListViewModel {
    masrafs: MasrafViewModel[];
    documents: DocumentViewModel[];
    messages: MessageViewModel[];
}
export interface EventViewModel {
    id: number;
    start: Date;
    title: string;
    users:UserModel[];
    rememberDate:Date;
    
}

export interface NotificationViewModel{
    id:number;
    message:string;
    isRead:boolean;
}

export const colors: any = {
    red: {
        primary: '#ad2121',
        secondary: '#FAE3E3'
    },
    blue: {
        primary: '#1e90ff',
        secondary: '#D1E8FF'
    },
    yellow: {
        primary: '#e3bc08',
        secondary: '#FDF1BA'
    }
};