export interface Contact {
  id: number;
  imageSrc: string;
  name: string;
  email: string;
  role: string;
  phone?: string;
  bio?: string;
  birthday?: string;
  selected: boolean;
  starred: boolean;
}
