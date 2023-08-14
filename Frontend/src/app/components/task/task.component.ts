import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Task } from 'src/app/models/task.model';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css'],
})
export class TaskComponent {
  @Input() taskData: Task;

  @Output() deleteEvent = new EventEmitter<number>();

  constructor(private taskService: TaskService) {}

  complete() {
    this.taskService
      .complete(this.taskData.id, true)
      .subscribe(() => (this.taskData.isComplete = true));
  }

  delete() {
    this.taskService
      .delete(this.taskData.id)
      .subscribe(() => this.deleteEvent.emit(this.taskData.id));
  }
}
