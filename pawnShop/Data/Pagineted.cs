public class Paginated<T> : List<T>
{
    public int PagiIni { get; set; }
    public int PagiTotal { get; set; }

    public Paginated(List<T> items, int count, int pagiIni, int pagiToltaREGI)
    {
        PagiIni = pagiIni;
        PagiTotal = (int)Math.Ceiling(count / (double)pagiToltaREGI);

        // Only add the items for the current page
        this.AddRange(items.Skip((pagiIni - 1) * pagiToltaREGI).Take(pagiToltaREGI));
    }

    public bool HasPreviousPage => PagiIni > 1;
    public bool HasNextPage => PagiIni < PagiTotal;
}
