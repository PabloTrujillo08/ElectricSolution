import { CustomUTCDatePipe } from './custom-utc.date.pipe';

describe('CustomUTCDatePipe', () => {
  it('create an instance', () => {
    const pipe = new CustomUTCDatePipe();
    expect(pipe).toBeTruthy();
  });
});
