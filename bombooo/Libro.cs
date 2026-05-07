namespace bombooo
{
    public class Libro
    {
        // Atributos privados
        private string _titulo;
        private string _autor;
        private int _anyo;
        private bool _disponible;

        // Constructor
        public Libro(string titulo, string autor, int anyo, bool disponible)
        {
            _titulo = titulo;
            _autor = autor;
            _anyo = anyo;
            _disponible = disponible;
        }

        // Propiedades públicas (solo lectura)
        public string Titulo => _titulo;
        public string Autor => _autor;
        public int Anyo => _anyo;
        public bool Disponible => _disponible;

        // Métodos get según el diagrama
        public string getTitulo()
        {
            return _titulo;
        }

        public string getAutor()
        {
            return _autor;
        }

        public int getAnyo()
        {
            return _anyo;
        }

        public bool isDisponible()
        {
            return _disponible;
        }

        // ToString() sobrescrito
        public override string ToString()
        {
            return $"{_titulo} - {_autor} ({_anyo})";
        }
    }
}
