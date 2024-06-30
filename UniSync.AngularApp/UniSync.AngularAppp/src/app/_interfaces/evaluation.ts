export interface Evaluation {
  studentId: string;
  courseId: string;
  professorId: string;
  grade: number;
  dateTime: Date;
  comment?: string;
}
