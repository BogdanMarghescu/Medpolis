   M S B A M L   `   `   `  ����C  ?Medpolis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MedpolisMedpolis  5          Medpolis.Login_page   5      n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:Medpolis     5      -   5          N�Name$  
login_page��5       N� Title$ 
Login_page��5	       � 	Resources 5
       �� 
TargetType   roundedpasswordbox �� CornerRadius J� SnapsToDevicePixels��%�   &	        �� 5   
   " G�6   .R�5      �� #��z��6   ���	#FFFF7171#��z��6W   $ 20��6�   ._�5      �� $��PART_ContentHost��6   # z 67   5      5      5      .��5      � ��5   
   �� 5       j� Stretch$ UniformToFill=�6    �� ImageSource$ login_background.jpg��62   .+�5   
   �� $
��Left=�6   $	��250��6+   $��
310,80,0,0q�68   $	��Top=�6L   $
��1300��6d   $��Medpolis.png��6q   �� 5   
   -   5         ��Name$ login��5      $��Autentificare��6    $��810,620,810,0q�68   $	��Top=�6O   $	��300��6g   $��60��6s   	 � Cursor$
	 Hand��6   ���	#FFFF7171$��Gill Sans MT)�6�   $��36(�6�   4  .System.Windows.Automation.AutomationProperties
   Name$
 login��6�   $��Center=�6  ���	#FF7F0000 5       �� 
TargetType �� Property �� Value��%_   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 20��65   5      5      ��5      �� 5       �� EndPoint$ 0.5,1&�6&    � MappingMode$ RelativeToBoundingBox=�67    �� 
StartPoint$ 0.5,0&�6[   .��5      �  � Color$ 	#FFF0B3B3��6#    � Offset$ 1Q�65   � 5      $ 	#FFEA2F2F��6#   5       5"      �� 5#   
   -   5        $ new_user��5#      $��Creare cont nou��6#   $��810,720,0,0q�6=   $	��Top=�6R   $	��300��6j   $��60��6v   $
	 Hand��6�   ���	#FFFF7171$��Gill Sans MT)�6�   $��36(�6�   $
 new_user��6�   $
��Left=�6  ���	#FF7F0000 5$      ��%_   '��       �� 5%      " ��6   .�5&      ��  �@��CornerRadius$ 20��65   5'      5(      ��5)      �� 5*      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5+      � $ 	#FFF0B3B3��6#   $ 1Q�65   � 5,      $ 	#FFEA2F2F��6#   5-      5/      �� 50   
   -   5         ��Name$ 	email_box��50      $
��Left=�6%   $��40��6@   $��760,430,0,0q�6L    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��400��6�   $��Center=�6�   $��24(�6�   $��Gill Sans MT)�6�   $��15,0,0,0q�6�   " �� VerticalScrollBarVisibility$ Disabled=�6�   ���	#FFFF7171���	#FF7F0000.��6L   51      ��%_   '��       �� 52      " ��6   .�53      ��  �@��CornerRadius$ 20��65   54      55      .��6!   ��56      �� 57      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��58      � $ White��6#   $ 0.352Q�61   � 59      $ 	#FFBB8181��6#   $ 1Q�65   5:      .��5;   "   5<      G� 5=   
   -   5         G�Name$ password_box��5=      #��[  6,   $
��Left=�6[   $��760,500,0,0q�6v   $	��Top=�6�   $	��400��6�   $��40��6�   $��Center=�6�   $��24(�6�   $��Gill Sans MT)�6�   $��Disabled=�6  $��15,0,0,0q�69  ���	#FFFF7171���	#FF7F0000��5>      �� 5?      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5@      � $ White��6#   $ 0.352Q�61   � 5A      $ 	#FFBB8181��6#   $ 1Q�65   5B      5D      �� 5E   
   $��Email:��6   $
��Left=�6!   $	��Top=�6<   $��608,430,0,0q�6T   $��40��6i   $	��140��6u   $��Center=�6�   $��Right=�6�   $��Gill Sans MT)�6�   $��26(�6�   ���	#FF7F00006�   �� 5F   
   $��Parolă:��6   $
��Left=�6"   $��608,500,0,0q�6=   $	��Top=�6R   $	��140��6j   $��40��6v   $��Center=�6�   $��Right=�6�   $��Gill Sans MT)�6�   $��26(�6�   ���	#FF7F00006�   �� 5G   
   -   5        $ login_doctor��5G      $��Autentificare ca doctor��6'   $��760,816,760,0q�6I   $	��Top=�6`   $	��400��6x   $��60��6�   $
	 Hand��6�   ���	#FFFF7171$��Gill Sans MT)�6�   $��36(�6�   $
 login_doctor��6�   $��Center=�6  ���	#FF7F0000 5H      ��%_   '��       �� 5I      " ��6   .�5J      ��  �@��CornerRadius$ 20��65   5K      5L      ��5M      �� 5N      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5O      � $ 	#FFF0B3B3��6#   $ 1Q�65   � 5P      $ 	#FFEA2F2F��6#   5Q      5S      5T      5U      