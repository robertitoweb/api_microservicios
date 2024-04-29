namespace api.Entities
{
    public class Base
    {
        public Guid Id { get; set; }
        public bool Habilitado { get; set; }    
        public DateTime FechaCreacion {  get; set; }    
        public DateTime FechaModificacion { get; set; }


    }
}
