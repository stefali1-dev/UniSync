export interface Evaluation {
  evaluationId: string;
  studentId: string;
  courseId: string;
  professorId: string;
  grade: number;
  dateTime: Date;
  comment?: string;
}
