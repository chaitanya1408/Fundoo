using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Model;
using FundooApp.Controllers;
using Moq;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestProject1
{
    public class NoteTestCases
    {
        NoteController notecontroller;
        private readonly INoteBL noteBL;
        public NoteTestCases()
        {
            var repository = new Mock<INoteRL>();
            this.noteBL = new NoteBL(repository.Object);
            notecontroller = new NoteController(this.noteBL);
        }

        public async Task ForBadRequestNoteCreation()
        {
            NoteModel data = new NoteModel()
            {
                Title = "kjcxfkl",
                Description = "zbcbza",
                Reminder = "2020-03-11T12:02:12.350Z",

            };
        }
    }
}
