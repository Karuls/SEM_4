   M S B A M L   `   `   `  ���� E  ACarSharing, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CarSharing.ViewCarSharing.View      CarSharing.View.Profile   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:CarSharing.View       � Title$  Profile��$	��700��$
��1000��$	��700��$	��600�� � WindowState$ 	Maximized=� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ Main_window�����	#FF100E3D � ColumnDefinitions �� $
��200* ��� $
��800* �.+�� $��3��$��0��$	��300�� � RowDefinitions �� $	��85* ��� $	��45* ��� $	��55* ��� $	��55* ��� $	��55* ��� $	��55* ��� $	��55* ��� $
��279* �.+��� $��0��._�(� $��Stretch=�.��� $��	10 15 4 5q�$��Stretch=� �� $	��21* ��� $	��41* �.+��� $/��)Q:\CarSharing\icons8-studiovinari-128.png���� $��1��$��72(�
�������$��	Twin Cars��$��	0,69,3,40q��� $��0,3,0,6q�$��1��._��� $��Content��$��Cambria Math)�$��18(� �� TextAlignment$ Center=����	#FFDFDFDF$��Bottom=�$
��Left=�$��	26,0,0,-5q��� -    ��Name$ 	Auto_Park��$��2��$��0,0,10,2q� �� CornerRadius$ 10��$	��300��._��� -   	 ��Name$	 Auto_ParkBatton��$��46��$��0,0,0,0q�
������ $
��Left=�.��� $	��148��$��42�� �� $	��11* ��� $	��29* �.+��� -   
 ��Name$
 Auto_ParkPNG��$��Q:\CarSharing\Wheel.png��$��35��$��35��$��0,0,3,0q��� -    ��Name$ Auto_ParkText��$��1��   	Auto Park#���   $��Center=�$��
Sitka Text)�$��18(����	#FFFFF6F6$��0,0,0,-4q��� -   $ 
Favourites��$��3��$��0,0,10,2q�$ 10��$	��300��._��� $��46��$��0,0,0,0q�
������ $
��Left=�.��� $	��148��$��42�� �� $	��11* ��� $	��29* �.+��� $��Q:\CarSharing\heart1.png��$��35��$��35��$��0,0,3,0q��� $��1��  
favourites#���  $��Center=�$��
Sitka Text)�$��18(����	#FFFFF6F6$��0,0,0,-4q��� -   $ Profile1��$��4��$��0,0,10,2q�$ 10��$	��300��._��� $��46��$��0,0,0,0q�
������ $
��Left=�.��� $	��148��$��42�� �� $	��11* ��� $	��29* �.+��� $��Q:\CarSharing\UserPng.png��$��35��$��35��$��0,0,3,0q��� $��1��  Profile#���  $��Center=�$��
Sitka Text)�$��18(����	#FFFFF6F6$��0,0,0,-4q��� -   $ Settings��$��5��$��0,0,10,2q�$ 10��$	��300��._��� $��46��$��0,0,0,0q�
������ $
��Left=�.��� $	��148��$��42�� �� $	��11* ��� $	��29* �.+��� $!��Q:\CarSharing\Settings2.png��$��35��$��35��$��0,0,3,0q��� $��1��  Settings#���  $��Center=�$��
Sitka Text)�$��18(����	#FFFFF6F6$��0,0,0,-4q��� -	   $ About��$��6��$��0,0,10,2q�$ 10��$	��300��._��� $��46��$��0,0,0,0q�
������ $
��Left=�.��� $	��148��$��42�� �� $	��11* ��� $	��29* �.+��� $��Q:\CarSharing\About.png��$��35��$��35��$��0,0,3,0q��� $��1�� 	 About#���  $��Center=�$��
Sitka Text)�$��18(����	#FFFFF6F6$��0,0,0,-4q��� $��1��$��3��$��
0 10 15 10q����	#FFDFDFDF$ 30��._�� -
   $ Right_art_Grid�� �� $��8* ��� $	��75* �.+�� $��0��$��3�� �� $	��63* �.+��� $��3��
�������$��0��$ 	30,30,0,0��$
}�True��._�� $��0�� �� $	��21* ��� $	��21* ��� $
��auto �$��10��.+��� $��0��._��� $��Center=�.+��� $
��Left=�#���  $��28(�$��Arial)� �� TextAlignment$ Center=�$��0,0,0,0q����	#FF161343$
��Bold"�$��5,0,0,0q�$��50��� $��2��$��80�� K� ZIndex$ 1��.+��� $��Right=�$��40��$��40��$��0,0,25,0q�����  �� ImageSource$ \Model\Images\Avatar.png�� �� RelativeTransform V� .~���  �� CenterY$	 0.5Q� �� CenterX$	 0.5Q� �� ScaleX$	 1.2Q� �� ScaleY$	 1.2Q���  �� CenterX$	 0.5Q� �� CenterY$	 0.5Q���  �� CenterX$	 0.5Q� �� CenterY$	 0.5Q�T� �� $��1��$��3��
�������$ 50,50,50,50��$��0,20,0,0q�._��  �� $
��385* ��� $
��384* �.+��� $��1�����	#FF100E3D$	��550��$��10,35,10,35q�$ 70,70,70,70��._��  �� �� �� �� �� �� �� �� �� .+��� $ 30��$��6�� J� Opacity$
 0.95Q�${�False�� J� Effect -  'System.Windows.Media.Effects.BlurEffect     Radius$ 95Q�����  ��  �� CacheOption$ OnLoad=� �� CreateOptions$ IgnoreImageCache=� �� 	UriSource$� zfile:///C:/Users/fedor/AppData/Local/Microsoft/VisualStudio/17.0_8f1dbffe/Designer/Cache/1-1636607834x86DD/View/Avatar.png?��� $��0��$��	User name��$
��Left=�$��Center=�$��24(����	#FF100E3D$
��Bold"�$��	35,10,0,0q��� $��1��$��login: 24 september 2024��$
��Left=�$	��Top=�$��20(�
�������$��20,0,0,0q�$��Cambria Math)��� $��2��$��2��
���   �$ 100,100,100,100��$	��100��$��Center=�$	��100��$
}�True��._��� $��Q:/CarSharing/avatar.png��$ 0.965Q� � Cursor$
 Hand��$��UniformToFill=� J� Clip @�   @� Center$  50,50&�! @� RadiusX$! 50Q�" @� RadiusY$" 50Q�� $��0�� �� �� .+��� ���	#FF100E3D$ 60��$��35,35,10,25q�$��0���� -   $ Bottom_Border_Profile�����	#FF100E3D$ 60��$��35,25,10,35q�$��1��._��  �� �� �� �� �� .+��� $��0��#���  $��35(�$��Arial)�$ Center=�$��0,0,0,0q�
�������$
��Bold"�$��5,0,0,0q�� -   $ Language_Field��$��1�� �� $	��95* ��� $	��79* �.+���   Language#���  $��27(�$��Arial)�$
 Left=�$��0,0,0,0q�
�������$��15,0,0,0q�$
��Left=�# �� SelectionOpacity$# 1Q�$|�False��$ J� 	AllowDrop$$ False��$	��122���� -   $
 	CountyPng��$��Q:/CarSharing/USA.png��$��37��$��37��$��UniformToFill=�$
 0.95Q�$��Center=�$
 Hand��
% � Tag$% US��$��Center=�$��	141,0,0,0q�� -   $ Theme_Field��$��2�� �� $	��95* ��� $	��79* �.+���  	 Theme#���  $��27(�$��Arial)�$
 Left=�$��0,0,0,0q�
�������$��15,0,0,0q�$
��Left=�$# 1Q�$|�False��$$ False��$	��122���� -   $
 ThemePng��$��Q:/CarSharing/Moon3.png��$��37��$��37��$��UniformToFill=�$
 0.95Q�$��Center=�$
 Hand��$
% Moon��$��Center=�$��	141,0,0,0q�� -   $ Sound_Field��$��3�� �� $	��95* ��� $	��79* �.+���  	 Sound#���  $��27(�$��Arial)�$
 Left=�$��0,0,0,0q�
�������$��15,0,0,0q�$
��Left=�$# 1Q�$|�False��$$ False��$	��122���� -   $
 SoundPng��$��Q:/CarSharing/sound.png��$��37��$��37��$��UniformToFill=�$ 0.965Q�$��Center=�$
 Hand��$% ON��$��Center=�$��	141,0,0,0q�