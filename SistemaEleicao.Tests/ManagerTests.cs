using Xunit;
using SistemaEleicao;

namespace SistemaEleicao.Tests
{
    public class ManagerTests
    {
        [Fact]
        public void CadastrarCandidato_ComNumeroInedito_DeveRetornarTrue()
        {
            // Arrange
            var manager = new EleicaoManager();

            // Act
            var resultado = manager.CadastrarCandidato("Candidato 1", 10);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void CadastrarCandidato_ComNumeroDuplicado_DeveRetornarFalse()
        {
            // Arrange
            var manager = new EleicaoManager();
            manager.CadastrarCandidato("Candidato 1", 10); // Cadastra o primeiro com o número 10

            // Act
            var resultadoDuplicado = manager.CadastrarCandidato("Candidato 2", 10); // Tenta duplicar o número 10

            // Assert
            Assert.False(resultadoDuplicado);
        }
    }
}