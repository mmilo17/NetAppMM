export interface Message {
    id: number;
    messageSenderId: number;
    messageSenderUsername: string;
    messageSenderPhotoUrl: string;
    messageRecipientId: number;
    messageRecipientUsername: string;
    messageRecipientPhotoUrl: string;
    content: string;
    isRead: boolean;
    dateRead: Date;
    messageSent: Date;
}
