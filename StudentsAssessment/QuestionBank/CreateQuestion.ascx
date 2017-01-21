<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CreateQuestion.ascx.vb" Inherits="StudentsAssessment.CreateQuestion" %>
<table id="tblQuestion" border="1" cellspacing="0" cellpadding="0">
		<colgroup>
			<col width="250px"/>
			<col width="200px"/>
		</colgroup>
		<tr class="trHeading">
			<td colspan="2">
                <div style="height:25px;background-color:mediumpurple;text-align:center;border:solid 2px white">
                     COMMON CORE STANDARD SCROLL
                </div>
            </td>
		</tr>
		<tr>
			<td colspan="2">
                <div id="divListofStandards">
                        <div class="divStandard"  id="divStandard1" runat="server">
                            <asp:linkbutton id="lnkStandard1" runat="server" Font-underline="False" ForeColor="White">STANDARD 1</asp:linkbutton>
                        </div><br/>
				        <div class="divStandard" id="divStandard2" runat="server">
					         <asp:linkbutton id="lnkStandard2" runat="server" Font-underline="False" ForeColor="White">STANDARD 2</asp:linkbutton>
				        </div><br/>
				        <div class="divStandard" id="divStandard3" runat="server">
					         <asp:linkbutton id="lnkStandard3" runat="server" Font-underline="False" ForeColor="White">STANDARD 3</asp:linkbutton>
				        </div><br/>
                </div>
			<td>
		<tr class="trHeading">
			<td colspan="2">
                 <div style="height:25px;background-color:mediumpurple;text-align:center;border:solid 2px white">
                     QUESTION TYPE SCROLL
                </div>
			</td>
		</tr>
		<tr>
			<td colspan="2">
                <div id="divListofAnstypes">
                    <div class="divAnswertype" id="divAnswertype3" runat="server">
                        <asp:linkbutton id="lnkAnswertype3" runat="server" Font-underline="False" ForeColor="White">MULTIPLE CHOICE</asp:linkbutton>
				    </div><br/>
				    <div class="divAnswertype" id="divAnswertype2" runat="server">
                        <asp:linkbutton id="lnkAnswertype2" runat="server" Font-underline="False" ForeColor="White">TRUE OR FALSE</asp:linkbutton>
				    </div><br/>
				    <div class="divAnswertype" id="divAnswertype4" runat="server">
                       <asp:linkbutton id="lnkAnswertype4" runat="server" Font-underline="False" ForeColor="White">MULTI-CHOICE</asp:linkbutton>
				    </div><br/>
				    <div class="divAnswertype" runat="server" id="divAnswertype1">
                      <asp:linkbutton id="lnkAnswertype1" runat="server" Font-underline="False" ForeColor="White">SHORT ANSWER (TENTATIVE)</asp:linkbutton>					   
				    </div><br/>
                </div>
			<td>
		</tr>
		<tr>
			<td class="divCreateQActions">ADD AUDIO OF QUESTION?</td>
			<td class="divCreateQActions">INSERT FORMULA</td>
		</tr>
		<tr>
			<td colspan="2">
                <asp:TextBox id="txtQuestionStem" runat="server" textMode="MultiLine" Rows="5"  Width="550px"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID ="txtQuestionStem" WatermarkText="TEXT BOX FOR TYPING QUESTION STEM" runat="server" WatermarkCssClass ="watermarked"  />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<div id="divMultipleChoices" runat="server">
					<table id="tblAnswers" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td colspan="2">Provide wordings for answer choices and select ONE of them as answer:</td>
                        </tr>
						<tr>
							<td><asp:RadioButton ID="rbtnChoiceA" runat="server" GroupName="grpChoice" /></td>
							<td>
								<div class="divAnswer">
                                    <asp:textbox ID="txtChoiceA" runat="server" Width="500px"></asp:textbox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID ="txtChoiceA" WatermarkText="CHOICE A" runat="server" WatermarkCssClass ="watermarked" />
								</div>
							</td>
						</tr>
						<tr>
							<td><asp:RadioButton ID="rbtnChoiceB" runat="server" GroupName="grpChoice" /></td>
							<td>
								<div class="divAnswer">
                                    <asp:textbox ID="txtChoiceB" runat="server" Width="500px"></asp:textbox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" TargetControlID ="txtChoiceB" WatermarkText="CHOICE B" runat="server" WatermarkCssClass ="watermarked" />
								</div>
							</td>
						</tr>
						<tr>
							<td><asp:RadioButton ID="rbtnChoiceC" runat="server" GroupName="grpChoice" /></td>
							<td>
								<div class="divAnswer">
                                    <asp:textbox ID="txtChoiceC" runat="server" Width="500px"></asp:textbox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" TargetControlID ="txtChoiceC" WatermarkText="CHOICE C" runat="server" WatermarkCssClass ="watermarked" />
								</div>
							</td>
						</tr>
						<tr>
							<td><asp:RadioButton ID="rbtnChoiceD" runat="server" GroupName="grpChoice" /></td>
							<td>
								<div class="divAnswer">
                                    <asp:textbox ID="txtChoiceD" runat="server" Width="500px"></asp:textbox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" TargetControlID ="txtChoiceD" WatermarkText="CHOICE D" runat="server" WatermarkCssClass ="watermarked" />
								</div>
							</td>
						</tr>
					</table><br/>
				</div>

                <div id="divMultiChoices" runat="server" visible="false">
					<table id="tblAnswersMultiCh" cellspacing="0" cellpadding="0" border="0">
                         <tr>
                            <td colspan="2">Provide wordings for answer choices and select ONE or MORE of them as answer:</td>
                        </tr>
						<tr>
							<td><asp:CheckBox ID="chkbtnChoiceA1" runat="server"/></td>
							<td>
								<div class="divAnswer">
                                    <asp:textbox ID="txtChoiceA1" runat="server" Width="500px"></asp:textbox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" TargetControlID ="txtChoiceA1" WatermarkText="CHOICE A" runat="server" WatermarkCssClass ="watermarked" />
								</div>
							</td>
						</tr>
						<tr>
							<td><asp:CheckBox ID="chkbtnChoiceB1" runat="server"/></td>
							<td>
								<div class="divAnswer">
                                    <asp:textbox ID="txtChoiceB1" runat="server" Width="500px"></asp:textbox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" TargetControlID ="txtChoiceB1" WatermarkText="CHOICE B" runat="server" WatermarkCssClass ="watermarked" />
								</div>
							</td>
						</tr>
						<tr>
							<td><asp:CheckBox ID="chkbtnChoiceC1" runat="server"/></td>
							<td>
								<div class="divAnswer">
                                    <asp:textbox ID="txtChoiceC1" runat="server" Width="500px"></asp:textbox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" TargetControlID ="txtChoiceC1" WatermarkText="CHOICE C" runat="server" WatermarkCssClass ="watermarked" />
								</div>
							</td>
						</tr>
						<tr>
							<td><asp:CheckBox ID="chkbtnChoiceD1" runat="server"/></td>
							<td>
								<div class="divAnswer">
                                    <asp:textbox ID="txtChoiceD1" runat="server" Width="500px"></asp:textbox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" TargetControlID ="txtChoiceD1" WatermarkText="CHOICE D" runat="server" WatermarkCssClass ="watermarked" />
								</div>
							</td>
						</tr>
					</table><br/>
				</div>

                <div id="divTrueFalse" runat="server" visible="false">
					<table id="tblAnswersTF" cellspacing="0" cellpadding="0" border="0">
                         <tr>
                            <td colspan="2">Provide wordings for answer choices and select EITHER true OR false as answer:</td>
                        </tr>
						<tr>
							<td>
                                <div class="divTFAnswer" style="margin-left:-450px;">
                                    <asp:RadioButton ID="rbtnTrue" runat="server" Text="True" GroupName="grpChoice1"/>
                                </div>
                            </td>
						</tr>
						<tr>
							<td>
                                <div class="divTFAnswer" style="margin-left:-450px;">
                                    <asp:RadioButton ID="rbtnFalse" runat="server" Text="False" GroupName="grpChoice1"/>
                                </div>
                            </td>
						</tr>
					</table><br/>
				</div>

                <div id="divShortAns" runat="server" visible="false">
					<table id="tblAnswersShortAns" cellspacing="0" cellpadding="0" border="0">
                         <tr>
                            <td colspan="2">Provide a short answer to this question in below field:</td>
                        </tr>
						<tr>
							<td colspan="2">
                                <asp:TextBox id="txtShortAns" runat="server" textMode="MultiLine" Rows="5"  Width="550px"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" TargetControlID ="txtShortAns" WatermarkText="TYPE SHORT ANSWER TO QUESTION HERE" runat="server" WatermarkCssClass ="watermarked"  />
							</td>
						</tr>
					</table><br/>
				</div>
			<td>
		</tr>
		<tr>
			<td class="divCreateQActions"><asp:LinkButton ID="lnkbtnAddImage"  runat="server" Font-underline="False" ForeColor="White">ADD IMAGE</asp:LinkButton></td>
			<td class="divCreateQActions"><asp:LinkButton ID="lnkbtnAddExplanation"  runat="server" Font-underline="False" ForeColor="White">ADD EXPLANATION</asp:LinkButton></td>
		</tr>
		<tr>
			<td class="divCreateQActions" colspan="2">
                    <asp:LinkButton ID="lnkCreateQuestion"  runat="server" Font-underline="False" ForeColor="White">CREATE QUESTION</asp:LinkButton>
			</td>
			
		</tr>
	</table>
