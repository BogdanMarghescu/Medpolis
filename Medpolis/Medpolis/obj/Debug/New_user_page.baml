   M S B A M L   `   `   `  ����C  ?Medpolis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MedpolisMedpolis  5          Medpolis.New_user_page   5      n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:Medpolis     5        N� Title$  New_user_page��5	       � 	Resources 5
       �� 
TargetType   roundedpasswordbox �� CornerRadius J� SnapsToDevicePixels��%�   &	        �� 5   
   " G�6   .R�5      �� #��z��6   ���	#FFFF7171#��z��6W   $ 20��6�   ._�5      �� $��PART_ContentHost��6   # z 67   5      5      5      .��5      � ��5   
   �� 5       j� Stretch$ UniformToFill=�6    �� ImageSource$ login_background.jpg��62   .+�5   
   �� $/��)Introduceți datele noului cont Medpolis:��6   $
��Left=�6C   $��310,330,0,0q�6^   $	��Top=�6s   $��Center=�6�   $��Center=�6�   $��Gill Sans MT)�6�   $��34(�6�   $
��Bold"�6�   ���	#FF7F00006  �� 5   
   $
��Left=�6   $	��250��6+   $��
310,80,0,0q�68   $	��Top=�6L   $
��1300��6d   $��Medpolis.png��6q   �� 5   
   -   5         ��Name$ new_user��5      $��Creare cont nou��6#   $��630,850,0,0q�6=   $	��Top=�6R   $	��300��6j   $��60��6v    � Cursor$
 Hand��6�   ���	#FFFF7171$��Gill Sans MT)�6�   $��36(�6�   4  .System.Windows.Automation.AutomationProperties	   Name$	 new_user��6�   $
��Left=�6  ���	#FF7F0000 5      
 �� 
TargetType �� Property �� Value��%_   '��       �� 5      "
 ��6   .�5      ��  �@��CornerRadius$ 20��65   5      5      ��5      �� 5        �� EndPoint$ 0.5,1&�6&    � MappingMode$ RelativeToBoundingBox=�67    �� 
StartPoint$ 0.5,0&�6[   .��5!      �  � Color$ 	#FFF0B3B3��6#    � Offset$ 1Q�65   � 5"      $ 	#FFEA2F2F��6#   5#      5%      �� 5'   
   $��Nume:��6   $
��Left=�6    $��560,400,0,0q�6;   $	��Top=�6P   $	��140��6h   $��40��6t   $��Center=�6�   $��Right=�6�   $��Gill Sans MT)�6�   $��26(�6�   ���	#FF7F00006�   �� 5(   
   -   5         ��Name$ surname_box��5(      $
��Left=�6'   $��40��6B   $��710,400,0,0q�6N    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��500��6�   $��Center=�6�   $��24(�6�   $��Gill Sans MT)�6�   $��15,0,0,0q�6�   " �� VerticalScrollBarVisibility$ Disabled=�6�   ���	#FFFF7171���	#FF7F0000.��6N   5)      ��%_   '��       �� 5*      "
 ��6   .�5+      ��  �@��CornerRadius$ 20��65   5,      5-      .��6!   ��5.      �� 5/      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��50      � $ White��6#   $ 0.352Q�61   � 51      $ 	#FFBB8181��6#   $ 1Q�65   52      .��53   "   54      �� 56   
   $��Prenume:��6   $
��Left=�6#   $��560,470,0,0q�6>   $	��Top=�6S   $	��140��6k   $��40��6w   $��Center=�6�   $��Right=�6�   $��Gill Sans MT)�6�   $��26(�6�   ���	#FF7F00006�   �� 57   
   -   5        $ name_box��57      $
��Left=�6$   $��40��6?   $��710,470,0,0q�6K   $
 Wrap=�6`   $	��Top=�6t   $	��500��6�   $��Center=�6�   $��24(�6�   $��Gill Sans MT)�6�   $��15,0,0,0q�6�   $ Disabled=�6�   ���	#FFFF7171���	#FF7F0000.��6K   58      ��%_   '��       �� 59      "
 ��6   .�5:      ��  �@��CornerRadius$ 20��65   5;      5<      .��6!   ��5=      �� 5>      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5?      � $ White��6#   $ 0.352Q�61   � 5@      $ 	#FFBB8181��6#   $ 1Q�65   5A      .��5B   "   5C      �� 5E   
   $
��CNP:��6   $
��Left=�6   $��560,540,0,0q�6:   $	��Top=�6O   $	��140��6g   $��40��6s   $��Center=�6   $��Right=�6�   $��Gill Sans MT)�6�   $��26(�6�   ���	#FF7F00006�   �� 5F   
   -   5        $ cnp_box��5F      $
��Left=�6#   $��40��6>   $��710,540,0,0q�6J   $
 Wrap=�6_   $	��Top=�6s   $	��500��6�   $��Center=�6�   $��24(�6�   $��Gill Sans MT)�6�   $��15,0,0,0q�6�   $ Disabled=�6�   ���	#FFFF7171���	#FF7F0000.��6J   5G      ��%_   '��       �� 5H      "
 ��6   .�5I      ��  �@��CornerRadius$ 20��65   5J      5K      .��6!   ��5L      �� 5M      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5N      � $ White��6#   $ 0.352Q�61   � 5O      $ 	#FFBB8181��6#   $ 1Q�65   5P      .��5Q   "   5R      �� 5T   
   $��Număr Telefon:��6   $
��Left=�6)   $��518,610,0,0q�6D   $	��Top=�6Y   $	��182��6q   $��40��6}   $��Center=�6�   $��Right=�6�   $��Gill Sans MT)�6�   $��26(�6�   ���	#FF7F00006�   �� 5U   
   -   5        $ 	phone_box��5U      $
��Left=�6%   $��40��6@   $��710,610,0,0q�6L   $
 Wrap=�6a   $	��Top=�6u   $	��500��6�   $��Center=�6�   $��24(�6�   $��Gill Sans MT)�6�   $��15,0,0,0q�6�   $ Disabled=�6�   ���	#FFFF7171���	#FF7F0000.��6L   5V      ��%_   '��       �� 5W      "
 ��6   .�5X      ��  �@��CornerRadius$ 20��65   5Y      5Z      .��6!   ��5[      �� 5\      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5]      � $ White��6#   $ 0.352Q�61   � 5^      $ 	#FFBB8181��6#   $ 1Q�65   5_      .��5`   "   5a      �� 5c   
   $��Email:��6   $
��Left=�6!   $	��Top=�6<   $��560,680,0,0q�6T   $��40��6i   $	��140��6u   $��Center=�6�   $��Right=�6�   $��Gill Sans MT)�6�   $��26(�6�   ���	#FF7F00006�   �� 5d   
   -   5        $ 	email_box��5d      $
��Left=�6%   $��40��6@   $��710,680,0,0q�6L   $
 Wrap=�6a   $	��Top=�6u   $	��500��6�   $��Center=�6�   $��24(�6�   $��Gill Sans MT)�6�   $��15,0,0,0q�6�   $ Disabled=�6�   ���	#FFFF7171���	#FF7F0000.��6L   5e      ��%_   '��       �� 5f      "
 ��6   .�5g      ��  �@��CornerRadius$ 20��65   5h      5i      .��6!   ��5j      �� 5k      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5l      � $ White��6#   $ 0.352Q�61   � 5m      $ 	#FFBB8181��6#   $ 1Q�65   5n      .��5o   "   5p      �� 5r   
   $��Parolă:��6   $
��Left=�6"   $��560,750,0,0q�6=   $	��Top=�6R   $	��140��6j   $��40��6v   $��Center=�6�   $��Right=�6�   $��Gill Sans MT)�6�   $��26(�6�   ���	#FF7F00006�   G� 5s   
   -   5         G�Name$ password_box��5s      #��[  6,   $
��Left=�6[   $��710,750,0,0q�6v   $	��Top=�6�   $	��500��6�   $��40��6�   $��Center=�6�   $��24(�6�   $��Gill Sans MT)�6�   $��Disabled=�6  $��15,0,0,0q�69  ���	#FFFF7171���	#FF7F0000��5t      �� 5u      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5v      � $ White��6#   $ 0.352Q�61   � 5w      $ 	#FFBB8181��6#   $ 1Q�65   5x      5z      �� 5{   
   -   5        $
 back��5{      $��Înapoi��6   $��971,850,0,0q�60   $	��Top=�6E   $	��300��6]   $��60��6i   $
 Hand��6u   ���	#FFFF7171$��Gill Sans MT)�6�   $��36(�6�   $	 new_user��6�   $
��Left=�6�   ���	#FF7F0000 5|      ��%_   '��       �� 5}      "
 ��6   .�5~      ��  �@��CornerRadius$ 20��65   5      5�      ��5�      �� 5�      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5�      � $ 	#FFF0B3B3��6#   $ 1Q�65   � 5�      $ 	#FFEA2F2F��6#   5�      5�      5�      5�      