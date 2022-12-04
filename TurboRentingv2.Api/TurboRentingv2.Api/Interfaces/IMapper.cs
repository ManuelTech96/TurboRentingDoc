namespace TurboRentingv2.Api.Interfaces
{
    public interface IMapper
    {
        /// <summary>
        /// Convierte una instancia de una clase en otro tipo dado
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source">Instancia a convertir</param>
        /// <returns>Instancia convertida</returns>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// Vuelca la información de una instancia en otra dada
        /// </summary>
        /// <param name="source">Instancia con la información a volcar</param>
        /// <param name="destination">Instancia sobre la que se volcará la información</param>
        /// <returns>Instancia resultado</returns>
        object Map(object source, object destination);

        /// <summary>
        /// Vuelca la información de una instancia en otra dada
        /// </summary>
        /// <param name="source">Instancia con la información a volcar</param>
        /// <param name="destination">Instancia sobre la que se volcará la información</param>
        /// <returns>Instancia resultado</returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
