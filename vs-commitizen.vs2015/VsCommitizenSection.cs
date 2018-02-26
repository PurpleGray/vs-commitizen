﻿using Microsoft.TeamFoundation.Controls;
using Microsoft.TeamFoundation.Controls.WPF.TeamExplorer;
using System;
using vs_commitizen.vs;
using vs_commitizen.vs.Models;
using vs_commitizen.vs.Extensions;
using System.Linq;

namespace vs_commitizen.vs2015
{
    [TeamExplorerSection(VsCommitizenSection.SectionId, VsCommitizenPage.PageId, 30)]
    public class VsCommitizenSection : TeamExplorerBaseSection
    {
        private const string SectionId = "50948F33-9223-4E8C-A8A5-37A6225AE4E2";

        public VsCommitizenView CommitizenSection => this.SectionContent as VsCommitizenView;

        public VsCommitizenSection()
        {
            this.Title = "Details";
            this.IsVisible = true;
            this.IsExpanded = true;
            this.IsBusy = false;
            this.SectionContent = new VsCommitizenView();
        }

        public override void Loaded(object sender, SectionLoadedEventArgs e)
        {
            var teamExplorer = GetService<ITeamExplorer>();
            var page = teamExplorer.CurrentPage as TeamExplorerBasePage;

            this.CommitizenSection.CommitExecuted += (s, ee) =>
            {
                AddNavigationValue(NavigationDataType.CommitData, new NavigationCommitModel
                {
                    AutoCommit = true, //TODO: add an option for this
                    Comment = CommitizenSection.GetComment()
                });

                teamExplorer.NavigateToPage(Guid.Parse(TeamExplorerPageIds.GitChanges), null);
            };
        }
    }
}