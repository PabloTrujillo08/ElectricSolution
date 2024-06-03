export const scaleRules: { [key: string]: object[] } = {
  'POWER_IO': [
    { rule: '%v < 0', backgroundColor: '#E53935' }, // Puedes añadir el texto en el componente
    { rule: '%v == 0', backgroundColor: 'black' }, // o manipularlo aquí como prefieras
    { rule: '%v >= 0', backgroundColor: '#12a14b' }
  ],
  'TEMP': [
    { rule: '%v < 20', backgroundColor: '#3498db' },
    { rule: '%v >= 20 && %v <= 50', backgroundColor: '#2ecc71' },
    { rule: '%v > 50', backgroundColor: '#e74c3c' }
  ],
  'POWER': [
    { rule: '%v >= 0', backgroundColor: '#12a14b' },
  ],
  'CONSUM': [
    { rule: '%v >= 0', backgroundColor: '#12a14b' },
  ],
  'PE_PW': [
    { rule: '%v >= 0', backgroundColor: '#12a14b' },
  ],
  'GRID_V': [
    { rule: '%v >= 0', backgroundColor: '#12a14b' },
  ],
};
