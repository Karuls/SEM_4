   M S B A M L   `   `   `  ����E  ACarSharing, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CarSharing.ViewCarSharing.View  5   	       CarSharing.View.laba6   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:CarSharing.View     5   	     � Title$  laba6��5   	   $	��450��6   $	��800��6$    � 	Resources 5
         MultiTriggerButtonStyle �� 
TargetType �� Property �� Value �� Triggers ]� 
Conditions �� Property �� Value��%,  &	        �� 5   
   " ��60   .�5      ��  � ��
 �����6+    5      ]� 5       5      �� 5       �@J�IsMouseOver$
 True��6;   �� 5       � ��$
 True��69   ./�5      ��  � ��
 � ���63   �� 5       � ��
 �   �63   5      5      5      .��5      � -   5        	 �Name$	 mainGrid��5      
�������.+�5   
   �� $��Center=�6   $��Center=�60   $	��300��6M   .+�5      �� -   5        
 ��Name$
 multiTriggerButton��5      $4��.MultiTrigger (наведите+нажмите)��5      #��[  5       $��40��5!      $��10q�6!   5#      �� 5'      -   5         ��Name$ dataTriggerCheck��5'      $)��#Активировать стиль��61   $��10q�6N   $��Center=�6Z   �� 5(      -   5         ��Name$ dataTriggerBorder��5(      
�������$��50��6G   $��10q�6S   ��5)      �� 5*      " ��6    5+      �� 5,       �� Binding 6*   �� *	IsChecked+ �� ElementName$ dataTriggerCheck�� �� Value True6f   .a�5-   "   ��  � �� �#4CAF506?   �� 5.   "    � ��
 � d �6@   �� 5/   "    � ��$ 2q�6D   50      52      ._�54      �� $&�� Статус: неактивно��6   $��Center=�65   $��Center=�6P   ��55      �� 56      " ��6     57      �� 58   "    6.   �� *	IsChecked+$ dataTriggerCheck�� True6j   .a�59   &   ��  � ��$# Статус: активно!��6=   �� 5:   &    � ��
 �����6C   5;   #   5=      5?      5@      �� 5C      $#��EventTrigger (нажмите)��6   $��40��66   $��10q�6B    � Triggers 5D      :� 5E       :� RoutedEvent$ Button.Click��6#   .[�5F      �� .��5G      �� .��5H   "   ��  �� 
TargetName$ mainGrid��61    �� TargetProperty$0 *(Panel.Background).(SolidColorBrush.Color)�5I   /   	 �� To #BBDEFB5J   /    g� Duration$ 0:0:1D�6<    g� AutoReverse$
 True��6M   5K      5L      5M      5O      �� 5Q      -   5        $��Сбросить фон��5Q      $��40��6,   $��10q�68   6D   5R      5S      5T      