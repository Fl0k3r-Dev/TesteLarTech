namespace TesteLarTech.Domain.ViewModels
{
    public class PaginacaoViewModel<T>
    {
        public IEnumerable<T>? Results { get; set; }
        public string? TermoBusca { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanhoPaginas { get; set; } = 10;
        public int TotalLinhas { get; set; }

    }
}
