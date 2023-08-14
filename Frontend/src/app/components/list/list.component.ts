import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { concatMap, delay, firstValueFrom, of, tap } from 'rxjs';
import { Task } from 'src/app/models/task.model';
import { AuthService } from 'src/app/services/auth.service';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})
export class ListComponent implements OnInit {
  constructor(
    private taskService: TaskService,
    private authService: AuthService,
    private router: Router
  ) {}

  tasks: Task[] = [];

  titleInput = new FormControl('', [
    Validators.required,
    Validators.minLength(5),
    Validators.maxLength(100),
  ]);

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    if (!this.authService.user?.user_id) {
      this.router.navigate(['auth']);
    } else {
      this.taskService.get().subscribe((result) => {
        this.tasks = result;
      });
    }
  }

  async addTask(): Promise<void> {
    const id = await firstValueFrom(
      this.taskService.add(this.titleInput.value!)
    );

    this.tasks.unshift({
      id: id,
      title: this.titleInput.value!,
      isComplete: false,
      createdOn: new Date(Date.now()),
      finishedOn: null,
    });
  }

  deleteTask(id: number): void {
    const index = this.tasks.findIndex((t) => t.id == id);
    if (index > -1) {
      this.tasks.splice(index, 1);
    }
  }

  logout(): void {
    this.authService.logout().subscribe(() => {
      this.router.navigate(['/auth']);
    });
  }
}
