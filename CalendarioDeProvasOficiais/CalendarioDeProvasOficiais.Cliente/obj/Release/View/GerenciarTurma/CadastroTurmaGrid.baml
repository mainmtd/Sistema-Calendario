   M S B A M L   `   `   `  ���� &  "CalendarioDeProvasOficiais.ClienteN    HCalendarioDeProvasOficiais.Cliente.View.GerenciarTurma.CadastroTurmaGrid   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� .+�� $��10q�  � RowDefinitions  �� $��50 ��� $��80 ��� $��* �.+��� $��0��$��Gerenciar Turma��$��26(�$
��Bold"�$��Center=��� $��1�� �� Orientation$ 
Horizontal=�$
��Left=�$��
15 15 15 5q�.+��� $��15q����#4169E1$��14(�
���   �
�������$��Center=�$��80��$��25������ *	CmdNovo+.��NOVO�� $��15q����#4169E1$��14(�
���   �
�������$��Center=�$��80��$��25������ *	CmdEditar+.��EDITAR�� $��15q����	#FF1C5113$��14(�
���   �
�������$��Center=�$��80��$��25������ *CmdAtualizar+.��	ATUALIZAR�� $��15q����#FF4040$��14(�
���   �
�������$��Center=�$��80��$��25������ *
CmdExcluir+.��	EXCLUIR� $��2��$��
15 0 15 15q�$��Stretch=�$��Stretch=�.+��� -   $��	ListCurso��$��Stretch=�$��Stretch=����� *	ListTurma+ �� Mode$ TwoWay=� �� SelectedItem �� *TurmaSelecionada+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� View �� .P���  �� Width$	 120�� �� DisplayMemberBinding �� *Nome+$ TwoWay=�.���� $��3 0 1 0q�$
��Nome��$
��Left=��� $	 250�� �� *Curso+$ TwoWay=�.���� $��3 0 1 0q�$��Curso��$
��Left=��� $	 200�� �� *	TipoCurso+$ TwoWay=�.���� $��3 0 0 0q�$
��Tipo��$
��Left=�