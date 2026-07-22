import { ChangeDetectorRef, Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { AnimeNote } from '../../models/anime-note';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-note-modal',
  imports: [FormsModule],
  templateUrl: './note-modal.html',
  styleUrl: './note-modal.css',
})
export class NoteModal implements OnChanges {
  
  constructor(private http: HttpClient, private cdr:ChangeDetectorRef){}

  @Input()
  note?: AnimeNote

  @Output()
  close = new EventEmitter<void>();

  title = '';
  hero = '';
  source = '';
  watchedEpisode = 0;
  expectedEpisode = 0;
  date = 2025;

  isEdit: boolean = false;

  ngOnChanges(): void {

    if (this.note) {

      this.isEdit = true;
      this.title = this.note.title;
      this.hero = this.note.hero;
      this.source = this.note.source;
      this.watchedEpisode = this.note.watchedEpisode;
      this.expectedEpisode = this.note.expectedEpisode;
      this.date = this.note.createdAtYear;

    } else {

      this.isEdit = false;
      this.title = '';
      this.hero = '';
      this.source = '';
      this.watchedEpisode = 0;
      this.expectedEpisode = 0;
      this.date = new Date().getFullYear();

    }
  }

  closeModal(){
    this.close.emit();
  }

  save() {

  const session = localStorage.getItem("session");

  const note = {
    id: this.note?.id,
    title: this.title,
    hero: this.hero,
    source: this.source,
    watchedEpisode: this.watchedEpisode,
    expectedEpisode: this.expectedEpisode,
    createdAtYear: this.date
  };

  if (this.isEdit) {

    this.http.put(
      "http://45.155.102.22:7135/Note",
      note,
      {
        headers: {
          Session: session ?? ""
        }
      }
    ).subscribe({

      next: () => {
        this.closeModal();
        this.cdr.detectChanges();
      },

      error: (err) => {
        console.log(err);
      }

    });

  } else {

    this.http.post(
      "http://45.155.102.22:7135/Note",
      note,
      {
        headers: {
          Session: session ?? ""
        }
      }
    ).subscribe({

      next: () => {
        this.closeModal();
        this.cdr.detectChanges();
      },

      error: (err) => {
        console.log(err);
      }

    });

  }

}
}
