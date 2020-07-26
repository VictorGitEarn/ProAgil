using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
    public class EventoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Local é entre 3 e 100 caracteres")]
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "O Tema deve ser Preenchido")]
        public string Tema { get; set; }

        [Range(2, 120000)]
        public int QtdPessoas { get; set; }
        public string ImagemUrl { get; set; }
        public string Telefone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public List<LoteDTO> Lote { get; set; }
        public List<RedeSocialDTO> RedeSociais { get; set; }
        public List<PalestranteDTO> Palestrantes { get; set; }
    }
}