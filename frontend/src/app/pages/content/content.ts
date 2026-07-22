import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Calendar1, Database, Eye, LucideAngularModule, TimerIcon, User } from 'lucide-angular';
import { NoteModal } from '../note-modal/note-modal';
import { AnimeNote } from '../../models/anime-note';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-content',
  imports: [
    LucideAngularModule,
    NoteModal
  ],
  templateUrl: './content.html',
  styleUrl: './content.css',
})
export class Content implements OnInit {

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef){}

  readonly eye = Eye;
  readonly expected = TimerIcon;
  readonly hero = User;
  readonly source = Database;
  readonly date = Calendar1;

  selectedNote?: AnimeNote;

  notes: AnimeNote[] = []

  showModal: boolean = false;

  ngOnInit(): void {

  const session = localStorage.getItem("session");

  this.http.get<AnimeNote[]>(
    "http://45.155.102.22:7135/Note/all",
    {
      headers: {
        Session: session ?? ""
      }
    }
  ).subscribe({

    next: (result) => {

      this.notes = result;

      this.cdr.detectChanges();

    },

    error: (err) => {

      console.log(err);

    }

  });

}

  openModal(note?: AnimeNote){
    this.selectedNote = note;
    this.showModal = true;
  }

  closeModal(){
    this.showModal = false;
  }

  deleteNote(id: string) {

    const session = localStorage.getItem("session");

    this.http.delete(
        `http://45.155.102.22:7135/Note/${id}`,
        {
            headers: {
                Session: session ?? ""
            }
        }
    ).subscribe({

        next: () => {

            window.location.reload();

        },

        error: err => {

            console.log(err);

        }

    });

}
}
