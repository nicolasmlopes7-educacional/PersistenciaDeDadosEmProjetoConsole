using System.Text;
using GestaoJogos.Models;

namespace GestaoJogos.Repositories
{
    public class JogoRepository
    {
        private readonly string? _caminhoArquivo;

        public JogoRepository()
        {
            string pasta = Path.Combine(Environment.CurrentDirectory, "dados"); // 
            Directory.CreateDirectory(pasta);

            _caminhoArquivo = Path.Combine(pasta, "jogos.csv");

            if (!File.Exists(_caminhoArquivo))
            {
                File.WriteAllText(
                    _caminhoArquivo,
                    "Id;Nome;Desenvolvedora;Genero;Ano;Lancamneto;Preco" +
                    Environment.NewLine,
                    Encoding.UTF8
                );
            }
        }

        public List<Jogo> Listar()
        {
            var jogos = new List<Jogo>();

            var linhas = File.ReadAllLines(_caminhoArquivo, Encoding.UTF8);


            foreach (var linha in linhas.Skip(1)) //esse skip é por conta que existe um cabeçalho na primeira linha do csv
            {
                if (string.IsNullOrWhiteSpace(linha))
                {
                    continue;
                }

                var campos = linha.Split(';'); //O split pega uma string e corta a cada vez que encontrar o (;)

                if (campos.Length != 6)
                {
                    continue;
                }

                jogos.Add(new Jogo
                {
                    Id = int.Parse(campos[0]),
                    Nome = (campos[1]),
                    Desenvolvedora = (campos[2]),
                    Genero = (campos[3]),
                    AnoLancamento = int.Parse(campos[4]),
                    Preco = decimal.Parse(campos[5])
                });
            }
            
            return jogos;
        }
    }
}