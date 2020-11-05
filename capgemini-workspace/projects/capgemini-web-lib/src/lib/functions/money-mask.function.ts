export function moneyMask(value: number, withDollarSign: boolean = true): string {
  if (value === null || value === undefined){
    return null;
  }

  return `${withDollarSign ? 'R$ ' : ''}${value.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
}
