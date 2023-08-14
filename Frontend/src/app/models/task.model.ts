export interface Task {
  id: number;
  title: string;
  isComplete: boolean;
  createdOn: Date;
  finishedOn: Date | null;
}
