   M S B A M L   `   `   `  ����Y  UTranspoDocMonitor.Desktop.User, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:TranspoDocMonitor.Desktop.User.Views$TranspoDocMonitor.Desktop.User.Views  5      3    -TranspoDocMonitor.Desktop.User.Views.UserView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlocal2clr-namespace:TranspoDocMonitor.Desktop.User.Views     5      -   5          >�Name$  UserTab��5      .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Create User��6.   $��	10,10,0,0q�6c   ��6D   �� *
ShowDialog+6c   �� 5   
   $��0��6   $��1��6   $��Delete Selected��6.   $��	0,10,10,0q�6g   $��Right=�6z   ��6H   �� *
RemoveUser+6z   �� 5   
   -   5        $��UserListView��5   D   $��1��6   $��0��6    $��2��60   $��10q�6X   ��6d   �� *Users+ �� View 5      �� 5       .P�5!      �� $��ID��6%    �� Width$ 50��6U    �� DisplayMemberBinding 61   �� *ID+6U   �� 5"      $
��Name��6%   $	 150��6Y    63   �� *Name+6Y   �� 5#      $��Email��6%   $	 200��6[    64   �� *Email+6[   �� 5$      $��Select��6%    �� CellTemplate 5%      �� 5&      .R�5'   "   ��  d� 	IsChecked 6+   �� *
IsSelected+ �� Mode$ TwoWay=�5(      5*      5+      5-      5.      5/      