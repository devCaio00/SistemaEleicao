using Xunit;
using SistemaEleicao;

namespace SistemaEleicao.Tests
{
    public class CandidatoTests
    {
        [Fact]
        public void Candidato_DeveInicializarComZeroVotos()
        {
            // Arrange & Act
            var candidato = new Candidato("Prefeito Teste", 99);

            // Assert
            Assert.Equal("Prefeito Teste", candidato.Nome);
            Assert.Equal(99, candidato.Numero);
            Assert.Equal(0, candidato.VotosRecebidos);
        }

        [Fact]
        public void AdicionarVoto_DeveIncrementarVotosRecebidos()
        {
            // Arrange
            var candidato = new Candidato("Vereador Teste", 55);

            // Act
            candidato.AdicionarVoto();
            candidato.AdicionarVoto();

            // Assert
            Assert.Equal(2, candidato.VotosRecebidos);
        }
    }
}