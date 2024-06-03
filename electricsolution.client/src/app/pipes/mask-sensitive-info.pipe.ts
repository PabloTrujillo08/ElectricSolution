import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'maskSensitiveInfo'
})
export class MaskSensitiveInfoPipe implements PipeTransform {

  transform(value: string, visibleLength: number = 4 , hideWith: string = '...', finalVisible: number = 4): string {

    if (!value)
      return value;

    if (value.length <= visibleLength + finalVisible)
      return value;
    

    const startVisiblePart = value.substring(0, visibleLength);

    const endVisiblePart = value.substring(value.length - finalVisible);

    return `${startVisiblePart}${hideWith}${endVisiblePart}`;


  }
}
