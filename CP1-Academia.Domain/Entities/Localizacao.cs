using ClassLibrary1.Common;

namespace ClassLibrary1.Entities;

public class Localizacao : BaseEntity
{
    public string Estado { get; private set; }
    
    public string Cidade { get; private set; }
    
    public string Bairro { get; private set; }
    
    public string Cep { get; private set; }
    
    public string Rua { get; private set; }
    
    public int Numero { get; private set; }

    public Localizacao(string estado, string cidade, string bairro, string cep, string rua, int numero)
    {
        Estado = estado;
        Cidade = cidade;
        Bairro = bairro;
        Cep = cep;
        Rua = rua;
        Numero = numero;
    }
}