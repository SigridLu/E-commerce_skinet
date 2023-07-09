import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

// This component creates a global form validator interface that could validate Angular form elements.

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements ControlValueAccessor {
  @Input() type = 'text'; // by default, input property set to text, could change to password or other types depends on context.
  @Input() label = '';

  // @Self directive is used to tell DI to create new instance instead of reusing an existing instance.
  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }

  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }

  // For template to use controlDir from this component, because typescript in stick mode won't allow us to use NgControl directly in template.
  get control(): FormControl {
    return this.controlDir.control as FormControl
  }

}
