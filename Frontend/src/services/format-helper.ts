export function formatPlayingTime(days: number): string {
    
  if (days < 1) {
    return 'Menos de 1 dia';
  }

  if (days < 30) {
    return days === 1 ? '1 dia' : `${days} dias`;
  }

  if (days < 365) {
    const months = Math.floor(days / 30); // Math.floor arredonda para baixo
    return months === 1 ? '1 mês' : `${months} meses`;
  }

  const years = Math.floor(days / 365);
  return years === 1 ? '1 ano' : `${years} anos`;
}

// Formata números no padrão brasileiro (ex.: 15835 -> "15.835")
export function formatNumber(value: number): string {
  return value.toLocaleString('pt-BR', { maximumFractionDigits: 2 });
}