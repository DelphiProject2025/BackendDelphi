using System;
using System.Threading.Tasks;
using delphibackend.IAM.Domain.Repositories;
using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Delphi.Domain.Services;

namespace delphibackend.Delphi.Application.Internal.CommandServices
{
    public class QuestionCommandService : IQuestionCommandService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IRoomRepository _roomRepository;

        public QuestionCommandService(IQuestionRepository questionRepository, IParticipantRepository participantRepository, IRoomRepository roomRepository)
        {
            _questionRepository = questionRepository;
            _participantRepository = participantRepository;
            _roomRepository = roomRepository;
        }

        public async Task<Guid> Handle(CreateQuestionCommand command)
        {

            try
            {

           
                var participant = await _participantRepository.FindByIdAsync(command.ParticipantId);
                var room = await _roomRepository.FindByIdAsync(command.RoomId);
    
            if (participant == null || room == null)
            {
                throw new InvalidOperationException("Participant or Room not found.");
            }

                var question = new Question(command.ParticipantId, command.RoomId, command.Text);
                await _questionRepository.AddAsync(question);
            return question.Id;
            }
             catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task Handle(LikeQuestionCommand command)
        {
            var question = await _questionRepository.FindByIdAsync(command.QuestionId);
            if (question == null)
            {
                throw new InvalidOperationException("Question not found.");
            }

            question.Like();
            await _questionRepository.UpdateAsync(question);
        }

        public async Task Handle(AnswerQuestionCommand command)
        {
            var question = await _questionRepository.FindByIdAsync(command.id);
            if (question == null)
            {
                throw new InvalidOperationException("Question not found.");
            }

            question.SetAnswer(command.answer);
            await _questionRepository.UpdateAsync(question);
        }

    }
}
