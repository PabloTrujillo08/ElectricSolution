import { MaskSensitiveInfoPipe } from './mask-sensitive-info.pipe';

describe('MaskSensitiveInfoPipe', () => {
  it('create an instance', () => {
    const pipe = new MaskSensitiveInfoPipe();
    expect(pipe).toBeTruthy();
  });
});
